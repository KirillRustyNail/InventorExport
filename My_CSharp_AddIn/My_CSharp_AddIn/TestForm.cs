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

namespace My_CSharp_AddIn
{
    public partial class TestForm : Form
    {
        private Inventor.Application m_inventorAplication;

        public TestForm(Inventor.Application application)
        {
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



        }
    }
}
