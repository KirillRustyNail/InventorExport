
namespace My_CSharp_AddIn
{
    partial class ExportForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.ExportBut = new System.Windows.Forms.Button();
            this.PathBut = new System.Windows.Forms.Button();
            this.PathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ResolutionComBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DoSubAssembleComBox = new System.Windows.Forms.ComboBox();
            this.SimpleBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(136, 235);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Show";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ExportBut
            // 
            this.ExportBut.Location = new System.Drawing.Point(31, 235);
            this.ExportBut.Name = "ExportBut";
            this.ExportBut.Size = new System.Drawing.Size(99, 23);
            this.ExportBut.TabIndex = 3;
            this.ExportBut.Text = "Export";
            this.ExportBut.UseVisualStyleBackColor = true;
            this.ExportBut.Click += new System.EventHandler(this.button2_Click);
            // 
            // PathBut
            // 
            this.PathBut.Location = new System.Drawing.Point(384, 26);
            this.PathBut.Name = "PathBut";
            this.PathBut.Size = new System.Drawing.Size(29, 21);
            this.PathBut.TabIndex = 4;
            this.PathBut.Text = "...";
            this.PathBut.UseVisualStyleBackColor = true;
            this.PathBut.Click += new System.EventHandler(this.PathBut_Click);
            // 
            // PathTextBox
            // 
            this.PathTextBox.Enabled = false;
            this.PathTextBox.Location = new System.Drawing.Point(98, 25);
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.Size = new System.Drawing.Size(280, 20);
            this.PathTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Output Path";
            // 
            // ResolutionComBox
            // 
            this.ResolutionComBox.Items.AddRange(new object[] {
            "High",
            "Medium",
            "Low"});
            this.ResolutionComBox.Location = new System.Drawing.Point(171, 67);
            this.ResolutionComBox.Name = "ResolutionComBox";
            this.ResolutionComBox.Size = new System.Drawing.Size(121, 21);
            this.ResolutionComBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Models Resolution ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Export SubAssemblies as";
            // 
            // DoSubAssembleComBox
            // 
            this.DoSubAssembleComBox.Items.AddRange(new object[] {
            "One model",
            "Individual models"});
            this.DoSubAssembleComBox.Location = new System.Drawing.Point(171, 102);
            this.DoSubAssembleComBox.Name = "DoSubAssembleComBox";
            this.DoSubAssembleComBox.Size = new System.Drawing.Size(121, 21);
            this.DoSubAssembleComBox.TabIndex = 7;
            // 
            // SimpleBut
            // 
            this.SimpleBut.Location = new System.Drawing.Point(241, 235);
            this.SimpleBut.Name = "SimpleBut";
            this.SimpleBut.Size = new System.Drawing.Size(99, 23);
            this.SimpleBut.TabIndex = 2;
            this.SimpleBut.Text = "Simplefy(Test)";
            this.SimpleBut.UseVisualStyleBackColor = true;
            this.SimpleBut.Click += new System.EventHandler(this.SimpleBut_Click);
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 270);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DoSubAssembleComBox);
            this.Controls.Add(this.ResolutionComBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PathTextBox);
            this.Controls.Add(this.PathBut);
            this.Controls.Add(this.ExportBut);
            this.Controls.Add(this.SimpleBut);
            this.Controls.Add(this.button1);
            this.Name = "ExportForm";
            this.Text = "Export";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button ExportBut;
        private System.Windows.Forms.Button PathBut;
        private System.Windows.Forms.TextBox PathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ResolutionComBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox DoSubAssembleComBox;
        private System.Windows.Forms.Button SimpleBut;
    }
}