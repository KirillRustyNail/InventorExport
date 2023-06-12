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
    public class CommandFunctions
    {
        static string ConnectionStatus = null;
        static string UserKey = null;

        //ExportForm startup
        public static void RunForm()
        {
            ExportForm frm;

            try
            {
                if (!FormManager.IsFormOpen<ExportForm>())
                {
                    frm = new ExportForm(Globals.invApp, ConnectionStatus, UserKey);
                    frm.Show();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        //Connection form startup
        public static void RunFormConnect()
        {
            ConnectForm Confrm;

            try
            {
                if (!FormManager.IsFormOpen<ConnectForm>())
                {
                    using (Confrm = new ConnectForm())
                    {
                        if (Confrm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            ConnectionStatus = Confrm.Status;
                            UserKey = Confrm.userid;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

    

        //A class for form management
        private static class FormManager
        {
            //Check if the entered form is open 
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
}



