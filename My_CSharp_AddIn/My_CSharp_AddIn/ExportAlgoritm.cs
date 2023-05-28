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

        //Selecting the export method. Where Do Export Assemble takes 0 or 1.
        //0 - together
        //1 - individually
        public void DoExport(string path , int Resolution , int DoExportAssemble)
        {
            try
            {
                Resolution++;

                Inventor.AssemblyDocument ASs = (Inventor.AssemblyDocument)m_inventorAplication.ActiveDocument;

                if (DoExportAssemble == 0)
                {
                    ExportTogether(ASs.ComponentDefinition.Occurrences, path , Resolution);
                }
                else 
                {
                    ExportIndividually(ASs.ComponentDefinition.Occurrences, path , Resolution);
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Export models with sub assemble Together
        public void ExportTogether(Inventor.ComponentOccurrences incollect , string path , int Resolution)
        {
            IEnumerator Em = incollect.GetEnumerator();
            Inventor.ComponentOccurrence objOc;
            Inventor.PartDocument desiredPart = null;
            Inventor.AssemblyDocument AssdesiredPart = null;
            Inventor.TransientObjects oTO;
            Inventor.NameValueMap oOptions;
            Inventor.DataMedium oDataMedium;
            Inventor.TranslationContext oContext;
            Inventor.ApplicationAddIn oAppAddin = m_inventorAplication.ApplicationAddIns.ItemById["{F539FB09-FC01-4260-A429-1818B14D6BAC}"];
            Inventor.TranslatorAddIn addIn = (Inventor.TranslatorAddIn)oAppAddin;

            while (Em.MoveNext() == true)
            {
                objOc = (Inventor.ComponentOccurrence)Em.Current;

                oTO = m_inventorAplication.TransientObjects;

                oContext = oTO.CreateTranslationContext();

                oContext.Type = Inventor.IOMechanismEnum.kFileBrowseIOMechanism;

                oOptions = oTO.CreateNameValueMap();

                oDataMedium = oTO.CreateDataMedium();

                oOptions.Value["ExportUnits"] = 0;

                oOptions.Value["Resolution"] = Resolution; // Resolution export models

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

                        throw;
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
                else if (objOc.DefinitionDocumentType == Inventor.DocumentTypeEnum.kAssemblyDocumentObject)
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

        // Export models with sub assemble Individuall
        public void ExportIndividually(Inventor.ComponentOccurrences incollect, string path, int Resolution)
        {
            IEnumerator Em = incollect.GetEnumerator();
            Inventor.ComponentOccurrence objOc;
            Inventor.PartDocument desiredPart = null;
            Inventor.AssemblyDocument AssdesiredPart = null;
            Inventor.TransientObjects oTO;
            Inventor.NameValueMap oOptions;
            Inventor.DataMedium oDataMedium;
            Inventor.TranslationContext oContext;
            Inventor.ApplicationAddIn oAppAddin = m_inventorAplication.ApplicationAddIns.ItemById["{F539FB09-FC01-4260-A429-1818B14D6BAC}"];
            Inventor.TranslatorAddIn addIn = (Inventor.TranslatorAddIn)oAppAddin;

            while (Em.MoveNext() == true)
            {
                objOc = (Inventor.ComponentOccurrence)Em.Current;

                oTO = m_inventorAplication.TransientObjects;

                oContext = oTO.CreateTranslationContext();

                oContext.Type = Inventor.IOMechanismEnum.kFileBrowseIOMechanism;

                oOptions = oTO.CreateNameValueMap();

                oDataMedium = oTO.CreateDataMedium();

                oOptions.Value["ExportUnits"] = 0;

                oOptions.Value["Resolution"] = Resolution; // Resolution export models

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
                    catch (Exception)
                    {
                        throw;
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
                else if(objOc.DefinitionDocumentType == Inventor.DocumentTypeEnum.kAssemblyDocumentObject)
                {
                    oOptions.Value["ExportFileStructure"] = 1;

                    IEnumerator objconEnum = objOc.Constraints.GetEnumerator();
                    Inventor.ComponentOccurrences temp = (Inventor.ComponentOccurrences)objOc.SubOccurrences;

                    if (temp.Count > 0)
                    {
                        AssdesiredPart = (Inventor.AssemblyDocument)objOc.Definition.Document;

                        string SubAssemblePath = path + "\\Parts\\" + AssdesiredPart.DisplayName.Substring(0, AssdesiredPart.DisplayName.Length - 4) + "\\";
                        ExportIndividually((Inventor.ComponentOccurrences)objOc.SubOccurrences, SubAssemblePath, Resolution);
                    }

                   /* AssdesiredPart = (Inventor.AssemblyDocument)objOc.Definition.Document;

                    string outPath = path + "\\Parts\\" + AssdesiredPart.DisplayName;
                    string fileNameObj = outPath.Substring(0, outPath.Length - 4) + ".obj";
                    string fileNameMtl = outPath.Substring(0, outPath.Length - 4) + ".mtl";
                    string TrueFileName = objOc.Name;

                    oDataMedium.FileName = System.IO.Path.ChangeExtension(outPath, ".obj");*/

                    /*try
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
                        }*/
                }
            }



        }
    }
}
