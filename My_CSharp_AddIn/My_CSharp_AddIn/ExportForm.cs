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
using Microsoft.VisualBasic.Compatibility.VB6;
using System.IO;

namespace My_CSharp_AddIn
{
    public partial class ExportForm : Form
    {
        private Inventor.Application m_inventorAplication;

        public string path;
        public ExportForm(Inventor.Application application)
        {
            
            TopMost = true;

            InitializeComponent();

            m_inventorAplication = application;

            PathTextBox.Text = m_inventorAplication.ActiveDocument.FullFileName.Replace(m_inventorAplication.ActiveDocument.DisplayName , "");
            path = PathTextBox.Text;

            ResolutionComBox.SelectedIndex = 2;
            ResolutionComBox.DropDownStyle = ComboBoxStyle.DropDownList;

            DoSubAssembleComBox.SelectedIndex = 0;
            DoSubAssembleComBox.DropDownStyle = ComboBoxStyle.DropDownList;

            FilletRadiusLabel.Enabled = false;
            FilletRadiusCount.Enabled = false;
        }

        private void TestForm_Load(object sender, EventArgs e)
        {

        }

     

        /*private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string Name = m_inventorAplication.ActiveDocument.DisplayName;

                try
                {

                    //Inventor.AssemblyDocument ASs = (Inventor.AssemblyDocument)m_inventorAplication.ActiveDocument;
                    //getComponent(ASs.ComponentDefinition.Occurrences, "no");

                    path = PathTextBox.Text + "\\" + m_inventorAplication.ActiveDocument.DisplayName.Substring(0, m_inventorAplication.ActiveDocument.DisplayName.Length - 4) + "_ExportResult";

                    AssemblyRecord assemblyRecord = new AssemblyRecord();

                    assemblyRecord.GetAssemble(path);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("The assembly is not open");
            }

        }*/

        private void button2_Click(object sender, EventArgs e)
        {
            //Inventor.AssemblyDocument oAssDoc = (Inventor.AssemblyDocument)m_inventorAplication.ActiveDocument;

            if(m_inventorAplication.ActiveDocument == null)
            {
            
                MessageBox.Show("The assembly is not open");
                return;
            }

            if (m_inventorAplication.ActiveDocument.DocumentType != Inventor.DocumentTypeEnum.kAssemblyDocumentObject)
            {
                MessageBox.Show("The assembly is not open");
                return;
            }

            if (!(string.IsNullOrEmpty(PathTextBox.Text) || string.IsNullOrWhiteSpace(PathTextBox.Text)))
            { 
                int counter = 0;
                string Filename = m_inventorAplication.ActiveDocument.DisplayName.Substring(0, m_inventorAplication.ActiveDocument.DisplayName.Length - 4) + "_ExportResult";
                string TempFileName = Filename;

                while (Directory.Exists(Path.Combine(PathTextBox.Text, TempFileName)))
                {
                    TempFileName = Filename + "(" + counter + ")";
                    counter++;
                }

                if (counter != 0)
                {
                    path = PathTextBox.Text + "\\" + TempFileName + "\\";
                }
                else
                {
                    path = PathTextBox.Text + "\\" + m_inventorAplication.ActiveDocument.DisplayName.Substring(0, m_inventorAplication.ActiveDocument.DisplayName.Length - 4) + "_ExportResult\\";
                }

               try
               {

                   AssemblyRecord assemblyRecord = new AssemblyRecord();

                   assemblyRecord.GetAssemble(path);

               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }

               try
               {
                   FilletRemover rem = new FilletRemover();

                   if (FilletRemoveBox.Checked == true)
                   {
                       rem.RemoveFiller(((double)FilletRadiusCount.Value));
                   }
               }
               catch (Exception ex)
               { 
                   MessageBox.Show("Error " + ex.Message);
               }

               try
               {
                   ExportAlgoritm export = new ExportAlgoritm();
                   export.DoExport(path, ResolutionComBox.SelectedIndex, DoSubAssembleComBox.SelectedIndex);
                   MessageBox.Show("Successfully");
               }
               catch (Exception ex)
               {
                   MessageBox.Show("Error "+ ex.Message);
               }               
            }
            else
            {
                MessageBox.Show("The path is not chosen");
            }
            

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

        private void PathBut_Click(object sender, EventArgs e)
        {
            Inventor.AssemblyDocument oAssDoc = (Inventor.AssemblyDocument)m_inventorAplication.ActiveDocument;

            string PATH;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select Folder";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                PATH = fbd.SelectedPath;
                PathTextBox.Text = PATH;
            }
            else
            {
                return;
            }
        }

        private void FilletRemoveBox_CheckedChanged(object sender, EventArgs e)
        {
            if (FilletRemoveBox.Checked == true)
            {
                FilletRadiusLabel.Enabled = true;
                FilletRadiusCount.Enabled = true;
            }
            else
            {
                FilletRadiusLabel.Enabled = false;
                FilletRadiusCount.Enabled = false;
            }
        }
    }

    
}
