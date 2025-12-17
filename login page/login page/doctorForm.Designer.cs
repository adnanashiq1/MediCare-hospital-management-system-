using System;
using System.Drawing;
using System.Windows.Forms;

namespace login_page
{
    partial class DoctorForm : Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // DoctorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 550);
            Name = "DoctorForm";
            Text = "Doctor Management";
            Load += DoctorForm_Load;
            ResumeLayout(false);
        }
    }
}
