using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace B32Assembler
{
    public partial class AssemblerForm : Form
    {
        public AssemblerForm()
        {
            InitializeComponent();

            txtOrigin.Text = "1000";
        }

        private void btnSourceBrowse_Click(object sender, EventArgs e)
        {
            this.fdSourceFile.ShowDialog();
            this.txtSourceFileName.Text = fdSourceFile.FileName;
        }

        private void btnOutputBrowse_Click(object sender, EventArgs e)
        {
            this.fdDestinationFile.ShowDialog();
            this.txtOutputFileName.Text = fdDestinationFile.FileName;
        }

        private void btnAssemble_Click(object sender, EventArgs e)
        {
            Assembler.Assemble(this.fdSourceFile.FileName, this.fdDestinationFile.FileName, this.txtOrigin.Text);
            MessageBox.Show("Done!");
        }
    }
}
