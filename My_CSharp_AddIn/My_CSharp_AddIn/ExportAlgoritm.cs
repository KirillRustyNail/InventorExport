using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_CSharp_AddIn
{
    class ExportAlgoritm
    {
        Inventor.Application m_inventorAplication = Globals.invApp;
        public void DoExport(string path)
        {
            try
            {

                Inventor.AssemblyDocument ASs = (Inventor.AssemblyDocument)m_inventorAplication.ActiveDocument;
                test(ASs.ComponentDefinition.Occurrences, path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void ExportLow(string path)
        {
            Inventor.AssemblyDocument oAssDoc = (Inventor.AssemblyDocument)m_inventorAplication.ActiveDocument;

            

            Inventor.TransientObjects oTO = m_inventorAplication.TransientObjects;
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

            oDataMedium.FileName = System.IO.Path.ChangeExtension(path + "\\Parts\\" + oAssDoc.DisplayName, ".obj");

            try
            {
                addIn.SaveCopyAs(oAssDoc, oContext, oOptions, oDataMedium);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        void test(Inventor.ComponentOccurrences incollect , string path)
        {
            IEnumerator Em = incollect.GetEnumerator();
            Inventor.ComponentOccurrence objOc;
            Inventor.PartDocument desiredPart = null;
            Inventor.AssemblyDocument AssdesiredPart = null;

            int count = 0;

            while (Em.MoveNext() == true)
            {
                objOc = (Inventor.ComponentOccurrence)Em.Current;
                

                Inventor.TransientObjects oTO = m_inventorAplication.TransientObjects;
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

                

                if (objOc.DefinitionDocumentType == Inventor.DocumentTypeEnum.kPartDocumentObject)
                {
                    oOptions.Value["ExportFileStructure"] = 1;

                    desiredPart = (Inventor.PartDocument)objOc.Definition.Document;

                    string outPath = path + "\\Parts\\" + desiredPart.DisplayName;
                    string fileNameObj = outPath.Substring(0, outPath.Length - 4) + ".obj";
                    string fileNameMtl = outPath.Substring(0, outPath.Length - 4) + ".mtl";
                    string TrueFileName = objOc.Name;

                    oDataMedium.FileName = System.IO.Path.ChangeExtension(outPath, ".obj");

                    try
                    {
                        addIn.SaveCopyAs(desiredPart, oContext, oOptions, oDataMedium);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }

                    if (File.Exists(fileNameObj)) 
                    {
                        File.Move(fileNameObj, path +"\\Parts\\"+TrueFileName.Replace(":", "_")+ ".obj");

                        if (File.Exists(fileNameMtl))
                        {
                            File.Move(fileNameMtl, path + "\\Parts\\" + TrueFileName.Replace(":", "_") + ".mtl");
                        }
                    }
                }
                else 
                {
                    oOptions.Value["ExportFileStructure"] = 0;

                    AssdesiredPart = (Inventor.AssemblyDocument)objOc.Definition.Document;

                    string outPath = path + "\\Parts\\" + AssdesiredPart.DisplayName;
                    string fileNameObj = outPath.Substring(0, outPath.Length - 4) + ".obj";
                    string fileNameMtl = outPath.Substring(0, outPath.Length - 4) + ".mtl";
                    string TrueFileName = objOc.Name;

                    oDataMedium.FileName = System.IO.Path.ChangeExtension(outPath, ".obj");

                    try
                    {
                        addIn.SaveCopyAs(AssdesiredPart, oContext, oOptions, oDataMedium);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }

                    if (File.Exists(fileNameObj))
                    {
                        File.Move(fileNameObj, path + "\\Parts\\" + TrueFileName.Replace(":", "_") + ".obj");

                        if (File.Exists(fileNameMtl))
                        {
                            File.Move(fileNameMtl, path + "\\Parts\\" + TrueFileName.Replace(":", "_") + ".mtl");
                        }
                    }
                }


                
            }
        }
    }

    
        
}
