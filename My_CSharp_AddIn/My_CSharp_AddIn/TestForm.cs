using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using Microsoft.VisualBasic.Compatibility.VB6;

namespace My_CSharp_AddIn
{
    public partial class TestForm : Form
    {
        private Inventor.Application m_inventorAplication;
        List<String> tree = new List<string>();
        public TestForm(Inventor.Application application)
        {
            
            TopMost = true;
            InitializeComponent();

            m_inventorAplication = application;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TestForm_Load(object sender, EventArgs e)
        {

        }

        void display()
        {
            String text = "";
            for (int i = 0; i < tree.Count(); i++)
            {
                text += " " + tree[i] + "\n";
            }

            richTextBox1.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Name = m_inventorAplication.ActiveDocument.DisplayName;

           

            try
            {

                Inventor.AssemblyDocument ASs = (Inventor.AssemblyDocument)m_inventorAplication.ActiveDocument;

             


                //getComponent(ASs.ComponentDefinition.Occurrences, "no");

                display();

                //MessageBox.Show(text);

                tree.Clear();

                AssemblyRecord assemblyRecord = new AssemblyRecord();
                assemblyRecord.GetAssemble();

                

                //Simple(ASs.ComponentDefinition.Occurrences);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        void Simple(Inventor.ComponentOccurrences incollect)
        {
            IEnumerator Em = incollect.GetEnumerator();
            Inventor.ComponentOccurrence objOc;
            Inventor.PartDocument desiredPart = null;
            Inventor.FilletFeature oFilletF = null;

            while (Em.MoveNext() == true)
            {
                objOc = (Inventor.ComponentOccurrence)Em.Current;

                if (objOc.DefinitionDocumentType == Inventor.DocumentTypeEnum.kPartDocumentObject)
                {
                    desiredPart = (Inventor.PartDocument)objOc.Definition.Document;

                    foreach (Inventor.PartFeature feature in desiredPart.ComponentDefinition.Features)
                    {
                        if (feature is Inventor.FilletFeature)
                        { 
                            try
                            {

                                oFilletF = (Inventor.FilletFeature)feature;
                                Inventor.ParametersEnumerator asds = oFilletF.Parameters;
                                Inventor.Parameter One = asds[1];

                                double val = Convert.ToDouble(One.Value);

                                if (val < 0.8)
                                {
                                    oFilletF.Delete();
                                }
                            }
                            catch (Exception)
                            {

                                MessageBox.Show("проблема с " + objOc._DisplayName + " В " + oFilletF.Name);
                            }

                        }
                    }
                }


                Simple((Inventor.ComponentOccurrences)objOc.SubOccurrences);
            }
        }
        private void getComponent(Inventor.ComponentOccurrences incollect, string SUB)
        {
            IEnumerator Em = incollect.GetEnumerator();

            Inventor.ComponentOccurrence objOc;

            Inventor.PartDocument desiredPart = null;
            Inventor.FilletFeature oFilletF = null;

            while (Em.MoveNext() == true)
            {
                objOc = (Inventor.ComponentOccurrence)Em.Current;

                double x = objOc.RangeBox.MaxPoint.X;
                double y = objOc.RangeBox.MaxPoint.Y;
                double z = objOc.RangeBox.MaxPoint.Z;

                

                if (SUB != "Sub")  tree.Add(objOc._DisplayName + "(" + objOc.Constraints.Count + ")" + "| x= " +x +" |y= " + y + " |z= " +z);
                else tree.Add("        "+objOc._DisplayName + "(" + objOc.Constraints.Count + ")" + "| x= " + x + " |y= " + y + " |z= " + z);

                IEnumerator objconEnum = objOc.Constraints.GetEnumerator();

                getComponent((Inventor.ComponentOccurrences)objOc.SubOccurrences , "Sub");

                display();

                getConstrains(objconEnum);
            }
        }

        private void getConstrains(IEnumerator objconEnum)
        {
            Inventor.AssemblyConstraint oAsscon;

            while (objconEnum.MoveNext() == true)
            {
                oAsscon = (Inventor.AssemblyConstraint)objconEnum.Current;
                tree.Add("   "+oAsscon.Name);

                display();

                Inventor.ComponentOccurrence One = (Inventor.ComponentOccurrence)oAsscon.OccurrenceOne;
                Inventor.ComponentOccurrence Two = (Inventor.ComponentOccurrence)oAsscon.OccurrenceTwo;
                
                if (One != null && Two != null)
                {

                   

                    var Type = oAsscon.GetType();

                   /* if (oAsscon.Type == Inventor.ObjectTypeEnum.kMateConstraintObject)
                    {
                        Inventor.MateConstraint mateConstraint = (Inventor.MateConstraint)oAsscon;

                        Inventor.FaceProxy faceProxy = (Inventor.FaceProxy)mateConstraint.EntityTwo;
                        if (faceProxy != null)
                        {

                            
                            var a = faceProxy.Geometry;
                            var b = faceProxy.Vertices;
                            var c = faceProxy.NativeObject.Vertices;

                            

                            foreach (Inventor.Vertex vertex in faceProxy.NativeObject.Vertices)
                            {
                                var v =vertex.Point.X;
                               // MessageBox.Show("Vertex: " +vertex.Point.X + " "+ vertex.Point.Y + " "+ vertex.Point.Z+" ");
                            }
                        }


                    }*/

                   /* Inventor.MateConstraint mateConstraint = (Inventor.MateConstraint)oAsscon;
                    Inventor.AngleConstraint angleConstraint = (Inventor.AngleConstraint)oAsscon;
                    Inventor.InsertConstraint insertConstraint = (Inventor.InsertConstraint)oAsscon;*/

                   

                    tree.Add("        {" + One._DisplayName);
                    tree.Add("        " + Two._DisplayName + "}"+"\n");
                }

                
               
            }

        }



       

        private void button2_Click(object sender, EventArgs e)
        {
            Inventor.AssemblyDocument oAssDoc = (Inventor.AssemblyDocument)m_inventorAplication.ActiveDocument;

            string PATH;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select Folder";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                PATH = fbd.SelectedPath;
            }
            else
            {
                MessageBox.Show("Must specify DXF path");
                return;
            }

            ExportAlgoritm export = new ExportAlgoritm();

            export.DoExport(PATH);

           /* Inventor.TransientObjects oTO = m_inventorAplication.TransientObjects;
            Inventor.TranslationContext oContext = oTO.CreateTranslationContext();

            Inventor.ApplicationAddIn oAppAddin = m_inventorAplication.ApplicationAddIns.ItemById["{F539FB09-FC01-4260-A429-1818B14D6BAC}"];


            Inventor.TranslatorAddIn addIn = (Inventor.TranslatorAddIn)oAppAddin;

            oContext.Type = Inventor.IOMechanismEnum.kFileBrowseIOMechanism;

            Inventor.NameValueMap oOptions = oTO.CreateNameValueMap();

            Inventor.DataMedium oDataMedium = oTO.CreateDataMedium();

            oOptions.Value["ExportUnits"] = 0;

            oOptions.Value["Resolution"] = 3;

            oOptions.Value["SurfaceDeviation"] = 0.16;

            oOptions.Value["NormalDeviation"] = 1500;

            oOptions.Value["MaxEdgeLength"] = 100000;

            oOptions.Value["AspectRatio"] = 2150;

            oOptions.Value["ExportFileStructure"] = 1;

            oDataMedium.FileName = System.IO.Path.ChangeExtension(PATH +"\\Parts\\" +oAssDoc.DisplayName, ".obj");

            try
            {
                addIn.SaveCopyAs(oAssDoc, oContext, oOptions, oDataMedium);
            }
            catch (Exception ex )
            {

                MessageBox.Show(ex.Message);
            }*/
        }
    }

    
}
