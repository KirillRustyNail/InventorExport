using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_CSharp_AddIn
{
    public partial class ConnectForm : Form
    {
    
        public string Status;// Connection to BD 
        public string userid;// User key for post result
        public ConnectForm()
        {
            InitializeComponent();
        }

        //Autorization
        async private void Connect_Button_Click(object sender, EventArgs e)
        {
            string Login = Login_text.Text;
            string Password = Password_text.Text;

            if (!String.IsNullOrEmpty(Login) && !String.IsNullOrEmpty(Password))
            {
                var result = await DBWebApi.GetConnect(Login, Password);

                Status = result[0];
                userid = result[1];
            }
            else 
            {
                MessageBox.Show("Login or Password not entered", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (Status == "OK")
            {
                MessageBox.Show("Successful connection", "Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error "+ Status, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

    }
}
