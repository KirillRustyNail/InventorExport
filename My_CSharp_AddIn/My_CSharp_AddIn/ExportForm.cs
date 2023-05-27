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
using System.IO;

namespace My_CSharp_AddIn
{
    public partial class ExportForm : Form
    {
        private Inventor.Application m_inventorAplication;

        public string path;
        public ExportForm(Inventor.Application application)
        {
            TopMost = true; // set the form on top of all other windows 

            InitializeComponent();

            m_inventorAplication = application;

            // Set SavePath
            PathTextBox.Text = m_inventorAplication.ActiveDocument.FullFileName.Replace(m_inventorAplication.ActiveDocument.DisplayName , "");
            path = PathTextBox.Text;

            // Set Resolution models
            // 0 - High , 1 - Medium, 2- Low
            ResolutionComBox.SelectedIndex = 2;
            ResolutionComBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // Set how export SubAssembles 
            // 0 - One model , 1 - Individual models
            DoSubAssembleComBox.SelectedIndex = 0;
            DoSubAssembleComBox.DropDownStyle = ComboBoxStyle.DropDownList;

            FilletRadiusLabel.Enabled = false;
            FilletRadiusCount.Enabled = false;
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {

            // Check if file is open
            if (m_inventorAplication.ActiveDocument == null)
            {
                MessageBox.Show("The assembly is not open");
                return;
            }

            //Check if an assembly file is open
            if (m_inventorAplication.ActiveDocument.DocumentType != Inventor.DocumentTypeEnum.kAssemblyDocumentObject)
            {
                MessageBox.Show("The assembly is not open");
                return;
            }

            //Checking the correctness of the saving path
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
                   //Hierarchy file entry
                   AssemblyRecord assemblyRecord = new AssemblyRecord();
                   assemblyRecord.GetAssemble(path);
               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }

               try
               {
                   // Removing Fillet
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
                   //Export models
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

        }

        // Select Folder
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
