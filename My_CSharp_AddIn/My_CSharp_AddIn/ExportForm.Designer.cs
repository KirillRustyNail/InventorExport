
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
            this.ExportBut = new System.Windows.Forms.Button();
            this.PathBut = new System.Windows.Forms.Button();
            this.PathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ResolutionComBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DoSubAssembleComBox = new System.Windows.Forms.ComboBox();
            this.FilletRemoveBox = new System.Windows.Forms.CheckBox();
            this.FilletRadiusCount = new System.Windows.Forms.NumericUpDown();
            this.FilletRadiusLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FilletRadiusCount)).BeginInit();
            this.SuspendLayout();
            // 
            // ExportBut
            // 
            this.ExportBut.Location = new System.Drawing.Point(31, 189);
            this.ExportBut.Name = "ExportBut";
            this.ExportBut.Size = new System.Drawing.Size(275, 23);
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
            // FilletRemoveBox
            // 
            this.FilletRemoveBox.AutoSize = true;
            this.FilletRemoveBox.Location = new System.Drawing.Point(31, 150);
            this.FilletRemoveBox.Name = "FilletRemoveBox";
            this.FilletRemoveBox.Size = new System.Drawing.Size(105, 17);
            this.FilletRemoveBox.TabIndex = 9;
            this.FilletRemoveBox.Text = "Do remove Fillet ";
            this.FilletRemoveBox.UseVisualStyleBackColor = true;
            this.FilletRemoveBox.CheckedChanged += new System.EventHandler(this.FilletRemoveBox_CheckedChanged);
            // 
            // FilletRadiusCount
            // 
            this.FilletRadiusCount.Location = new System.Drawing.Point(250, 149);
            this.FilletRadiusCount.Name = "FilletRadiusCount";
            this.FilletRadiusCount.Size = new System.Drawing.Size(56, 20);
            this.FilletRadiusCount.TabIndex = 10;
            // 
            // FilletRadiusLabel
            // 
            this.FilletRadiusLabel.AutoSize = true;
            this.FilletRadiusLabel.Location = new System.Drawing.Point(155, 151);
            this.FilletRadiusLabel.Name = "FilletRadiusLabel";
            this.FilletRadiusLabel.Size = new System.Drawing.Size(89, 13);
            this.FilletRadiusLabel.TabIndex = 11;
            this.FilletRadiusLabel.Text = "Fillet Radius (mm)";
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 237);
            this.Controls.Add(this.FilletRadiusLabel);
            this.Controls.Add(this.FilletRadiusCount);
            this.Controls.Add(this.FilletRemoveBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DoSubAssembleComBox);
            this.Controls.Add(this.ResolutionComBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PathTextBox);
            this.Controls.Add(this.PathBut);
            this.Controls.Add(this.ExportBut);
            this.Name = "ExportForm";
            this.Text = "Export";
            this.Load += new System.EventHandler(this.TestForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FilletRadiusCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ExportBut;
        private System.Windows.Forms.Button PathBut;
        private System.Windows.Forms.TextBox PathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ResolutionComBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox DoSubAssembleComBox;
        private System.Windows.Forms.CheckBox FilletRemoveBox;
        private System.Windows.Forms.NumericUpDown FilletRadiusCount;
        private System.Windows.Forms.Label FilletRadiusLabel;
    }
}