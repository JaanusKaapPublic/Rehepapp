using CoverageTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoveragePlanter
{
    public partial class Form1 : Form
    {
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        OpenFileDialog fileLoadDialog = new OpenFileDialog();

        public Form1()
        {
            InitializeComponent();
            txtBBL.Text = UiUtils.getExistingFile(new string[] { "*.bbl", "*.codeblocks" });
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

        private async void BtnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            txtLog.Text += "STARTING\r\n";
            txtLog.Update();
            BBL bbl = new BBL();
            bbl.load(txtBBL.Text);
            while (txtDir.Text.EndsWith("\\") || txtDir.Text.EndsWith("/"))
                txtDir.Text = txtDir.Text.Substring(0, txtDir.Text.Length - 1);
            Dictionary<string, int> result = bbl.plant(txtDir.Text);
            foreach (KeyValuePair<string, int> file in result)
            {
                txtLog.Text += file.Key + "\r\n";
                switch (file.Value)
                {
                    case 0:
                        break;
                    case 1:
                        txtLog.Text += "[NO FILE] " + file.Key + "\r\n";
                        break;
                    case 2:
                        txtLog.Text += "[READ/WRITE ERROR] " + file.Key + "\r\n";
                        break;
                    default:
                        txtLog.Text += "[UNKNOWN:"+file.Value+"] " + file.Key + "\r\n";
                        break;
                }
                txtLog.Update();
            }
            txtLog.Text += "DONE\r\n";
            btnStart.Enabled = true;
        }
    }
}
