﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

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

        private void button1_Click(object sender, EventArgs e)
        {
            string Name = m_inventorAplication.ActiveDocument.DisplayName;

            label1.Text = Name;

            try
            {

                Inventor.AssemblyDocument ASs = (Inventor.AssemblyDocument)m_inventorAplication.ActiveDocument;

               /*
                Inventor.ComponentOccurrence test = (Inventor.ComponentOccurrence)m_inventorAplication.ActiveDocument.SelectSet[1];


                // Получаем координаты выделенной детали
                double x = test.RangeBox.MaxPoint.X;
                double y = test.RangeBox.MaxPoint.Y;
                double z = test.RangeBox.MaxPoint.Z;*/

                //essageBox.Show(x + " " + y + " " + z);


                getComponent(ASs.ComponentDefinition.Occurrences);

                String text= "";
                for (int i = 0; i < tree.Count(); i++)
                {
                    text += " " + tree[i] + "\n";
                }


                MessageBox.Show(text);

                tree.Clear();

                CreadHint(1, 1, 1);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }
        private void getComponent(Inventor.ComponentOccurrences incollect)
        {
            IEnumerator Em = incollect.GetEnumerator();

            Inventor.ComponentOccurrence objOc;

            while (Em.MoveNext() == true)
            {
                objOc = (Inventor.ComponentOccurrence)Em.Current;

                double x = objOc.RangeBox.MaxPoint.X;
                double y = objOc.RangeBox.MaxPoint.Y;
                double z = objOc.RangeBox.MaxPoint.Z;

                tree.Add(objOc._DisplayName + "(" + objOc.Constraints.Count + ")" + "| x= " +x +" |y= " + y + " |z= " +z );

                IEnumerator objconEnum = objOc.Constraints.GetEnumerator();

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

                Inventor.ComponentOccurrence One = (Inventor.ComponentOccurrence)oAsscon.OccurrenceOne;
                Inventor.ComponentOccurrence Two = (Inventor.ComponentOccurrence)oAsscon.OccurrenceTwo;
                
                if (One != null && Two != null)
                {

                   

                    var Type = oAsscon.GetType();

                    if (oAsscon.Type == Inventor.ObjectTypeEnum.kMateConstraintObject)
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


                    }

                   /* Inventor.MateConstraint mateConstraint = (Inventor.MateConstraint)oAsscon;
                    Inventor.AngleConstraint angleConstraint = (Inventor.AngleConstraint)oAsscon;
                    Inventor.InsertConstraint insertConstraint = (Inventor.InsertConstraint)oAsscon;*/

                   

                    tree.Add("        {" + One._DisplayName);
                    tree.Add("        " + Two._DisplayName + "}"+"\n");
                }

                
               
            }

        }



        private void CreadHint(double x, double y, double z)
        {
            /*Inventor.TransientGeometry oTg = m_inventorAplication.TransientGeometry;

            Inventor.AssemblyDocument oDoc = (Inventor.AssemblyDocument)m_inventorAplication.ActiveDocument;

            Inventor.ComponentDefinition oCompDef = (Inventor.ComponentDefinition)oDoc.ComponentDefinition;

            Inventor.PlanarSketch oSketch = oCompDef.Sketches.Add(oCompDef.WorkPlanes[2]);

              oSketch.Name = "Connect";

              Inventor.Point2d centre = oTg.CreatePoint2d(x, y);

              Inventor.SketchCircle cicle = oSketch.SketchCircles.AddByCenterRadius(centre, 1);

              Inventor.Profile oProf = oSketch.Profiles.AddForSolid();

              Inventor.ExtrudeDefinition oExDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProf, Inventor.PartFeatureOperationEnum.kJoinOperation);

              oExDef.SetDistanceExtent(1, Inventor.PartFeatureExtentDirectionEnum.kPositiveExtentDirection);

              oCompDef.Features.ExtrudeFeatures.Add(oExDef);

            *//*Inventor.Point centre = oTg.CreatePoint(x, y, z);
            Inventor.Sphere Sphere = m_inventorAplication.TransientGeometry.CreateSphere(centre,1);*//*


            Inventor.Matrix sa = m_inventorAplication.TransientGeometry.CreateMatrix();


            Inventor.ComponentOccurrence hintA = oDoc.ComponentDefinition.Occurrences.AddByComponentDefinition(Sphere, sa);

            

            Inventor.Vector oTrans = oTg.CreateVector(1, 1, 1);
            sa.SetTranslation(oTrans);

*/


        }
    }

    
}
