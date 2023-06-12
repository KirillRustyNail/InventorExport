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
using System.IO.Compression;

namespace My_CSharp_AddIn
{
    public partial class ExportForm : Form
    {
        private Inventor.Application m_inventorAplication;
        private string statusCon; // Connection to BD status
        private string Userkey; // User key for post result

        public string path;
        public ExportForm(Inventor.Application application , string status, string userkey)
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
            
            if (status != null) Status_lable.Text = "Connection status:" + status;
            else Status_lable.Text = "Connection status: Not connect";
            statusCon = status;
            Userkey = userkey;
        }

        private void TestForm_Load(object sender, EventArgs e)
        {

        }

        private async void ExportButton_Click(object sender, EventArgs e)
        {
            DialogResult dr= new DialogResult();

            //Checking that the connection to the database is positive
            if (statusCon != "OK") dr = MessageBox.Show("The connection has not been established. The export will only be local.", "Export", MessageBoxButtons.YesNo);
            else dr = DialogResult.Yes;

            if (dr == DialogResult.Yes)
            {

                //Checking that a document is open
                if (m_inventorAplication.ActiveDocument == null)
                {

                    MessageBox.Show("The assembly is not open");
                    return;
                }

                //Checking that the document assembly is open 
                if (m_inventorAplication.ActiveDocument.DocumentType != Inventor.DocumentTypeEnum.kAssemblyDocumentObject)
                {
                    MessageBox.Show("The assembly is not open");
                    return;
                }

                //Checking that the path line is not empty 
                if (!(string.IsNullOrEmpty(PathTextBox.Text) || string.IsNullOrWhiteSpace(PathTextBox.Text)))
                {
                    int counter = 0; //count file with the same name
                    string Filename = m_inventorAplication.ActiveDocument.DisplayName.Substring(0, m_inventorAplication.ActiveDocument.DisplayName.Length - 4) + "_ExportResult";
                    string TempFileName = Filename;
                    bool SuccessWriteJson = false;// Success write Json file
                    bool SuccessExport = false; // Success export

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
                    
                    // try write assemble json file
                    try
                    {
                        AssemblyRecord assemblyRecord = new AssemblyRecord();
                        assemblyRecord.GetAssemble(path);
                        SuccessWriteJson = true;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //try remove fillet
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

                    //try export assembly
                    try
                    {
                        ExportAlgoritm export = new ExportAlgoritm();
                        export.DoExport(path, ResolutionComBox.SelectedIndex, DoSubAssembleComBox.SelectedIndex);
                        SuccessExport = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error " + ex.Message);
                    }

                    //try create zip file
                    if (SuccessExport && SuccessWriteJson)
                    { 
                        string Zipfilename = path.Substring(0, path.Length - 1) + ".zip";
                        ZipFile.CreateFromDirectory(path, Zipfilename);

                        if (statusCon == "OK")
                        {
                            var result = await DBWebApi.PostFile(Userkey, Zipfilename);
                            var Status = result[0];

                            if (Status != "OK")
                            {
                                MessageBox.Show("Connection Error \n It was saved only locally ", Status, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Status_lable.Text = "Connection status: Not connect";
                                statusCon = "Not connect";
                            }
                        }
                    }

                    if(SuccessExport && SuccessWriteJson) MessageBox.Show("Success export");
                    if(SuccessExport && !SuccessWriteJson) MessageBox.Show("Success export, but Json file write incorrectly");
                    if(!SuccessExport && SuccessWriteJson) MessageBox.Show("Export incorrectly, but Json file write success");
                    if(!SuccessExport && !SuccessWriteJson) MessageBox.Show("Export Failed");


                }
                else
                {
                    MessageBox.Show("The path is not chosen");
                }
            }
        }

        //Set file path 
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
