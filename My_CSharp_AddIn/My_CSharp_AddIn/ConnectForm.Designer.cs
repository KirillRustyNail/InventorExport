
namespace My_CSharp_AddIn
{
    partial class ConnectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.Password_text = new System.Windows.Forms.TextBox();
            this.Login_text = new System.Windows.Forms.TextBox();
            this.Connect_Button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Password_text);
            this.panel1.Controls.Add(this.Login_text);
            this.panel1.Controls.Add(this.Connect_Button);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 187);
            this.panel1.TabIndex = 3;
            // 
            // Password_text
            // 
            this.Password_text.Location = new System.Drawing.Point(69, 78);
            this.Password_text.Name = "Password_text";
            this.Password_text.Size = new System.Drawing.Size(307, 20);
            this.Password_text.TabIndex = 6;
            // 
            // Login_text
            // 
            this.Login_text.Location = new System.Drawing.Point(70, 46);
            this.Login_text.Name = "Login_text";
            this.Login_text.Size = new System.Drawing.Size(307, 20);
            this.Login_text.TabIndex = 7;
            // 
            // Connect_Button
            // 
            this.Connect_Button.Location = new System.Drawing.Point(12, 117);
            this.Connect_Button.Name = "Connect_Button";
            this.Connect_Button.Size = new System.Drawing.Size(364, 23);
            this.Connect_Button.TabIndex = 5;
            this.Connect_Button.Text = "Connect";
            this.Connect_Button.UseVisualStyleBackColor = true;
            this.Connect_Button.Click += new System.EventHandler(this.Connect_Button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Login";
            // 
            // ConnectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 210);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(432, 249);
            this.Name = "ConnectForm";
            this.Text = "Connect";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox Password_text;
        private System.Windows.Forms.TextBox Login_text;
        private System.Windows.Forms.Button Connect_Button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}