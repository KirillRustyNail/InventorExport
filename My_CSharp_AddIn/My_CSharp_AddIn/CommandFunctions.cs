// TODO: This module exists as a convenient location for the code that does the real
// work when a command is executed.

using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections;
using Inventor;
using Application = System.Windows.Forms.Application;

namespace My_CSharp_AddIn
{
    //Class of commands to be executed
    public static class CommandFunctions
    {
        //Run the addin form to export
        public static void RunExportForm()
        {
            ExportForm frm;

            try
            {
                if (!FormManager.IsFormOpen<ExportForm>())
                {
                    frm = new ExportForm(Globals.invApp);
                    frm.Show();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }

    //Class for working with forms
    public static class FormManager
    {
        //Check if the form is open
        public static bool IsFormOpen<T>() where T : Form
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(T))
                    return true;
            }
            return false;
        }
    }
}
