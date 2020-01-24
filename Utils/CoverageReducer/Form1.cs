using CoverageTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoverageReducer
{
    public partial class Form1 : Form
    {
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        OpenFileDialog fileLoadDialog = new OpenFileDialog();
        SaveFileDialog fileSaveDialog = new SaveFileDialog();

        public Form1()
        {
            InitializeComponent();
            txtBBL.Text = UiUtils.getExistingFile(new string[] { "*.bbl", "*.codeblocks" });
            txtDir.Text = UiUtils.getExistingDirectory(new string[] { "cov", "covs", "coverage", "coverages" });
            txtOutput.Text = Directory.GetCurrentDirectory() + "\\result.bbl";
        }

        private void BtnBBL_Click(object sender, EventArgs e)
        {
            if (fileLoadDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtBBL.Text = fileLoadDialog.FileName;
            }
        }

        private void BtnDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtDir.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void BtnOutput_Click(object sender, EventArgs e)
        {
            if (fileSaveDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtOutput.Text = fileSaveDialog.FileName;
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            txtLog.Text += "STARTING\r\n";
            txtLog.Update();
            BBL bbl = new BBL();

            bbl.load(txtBBL.Text);

            string[] files = Directory.GetFiles(txtDir.Text, "*.bbc");
            foreach(string file in files)
            {
                BBC bbc = new BBC();
                txtLog.Text += file + "\r\n";
                txtLog.Update();
                string error = bbc.load(file);
                if(error != null)
                {
                    txtLog.Text = "[ERROR]["+ file + "] " + error + "\r\n";
                    txtLog.Update();
                }
                bbl.removeBlocks(bbc);
            }
            bbl.save(txtOutput.Text);
            txtLog.Text += "DONE\r\n";
            btnStart.Enabled = true;
        }
    }
}
