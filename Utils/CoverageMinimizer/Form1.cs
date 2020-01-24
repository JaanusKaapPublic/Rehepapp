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

namespace CoverageMinimizer
{
    public partial class Form1 : Form
    {
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        public Form1()
        {
            InitializeComponent();
            txtCoverage.Text = UiUtils.getExistingDirectory(new string[] { "cov", "coverage", "coverages" });
            txtInput.Text = UiUtils.getExistingDirectory(new string[] { "input", "files" });
            txtOutput.Text = UiUtils.getExistingDirectory(new string[] { "out", "output", "results", "result" });
        }

        private void BtnCoverage_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtCoverage.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void BtnInput_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtInput.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void BtnOutput_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtOutput.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            txtLog.Text += "STARTED\r\n";
            List<string> results = new List<string>();
            Dictionary<string, BBC> coverages = new Dictionary<string, BBC>();

            string[] files = Directory.GetFiles(txtCoverage.Text, "*");
            string bestName = "";
            uint bestCount = 0;
            foreach (string file in files)
            {
                BBC tmp = new BBC();
                tmp.load(file);
                coverages[Path.GetFileName(file)] = tmp;

                if (tmp.getCount() > bestCount)
                {
                    bestCount = tmp.getCount();
                    bestName = Path.GetFileName(file);
                }
            }

            while (results.Count < Convert.ToUInt32(txtCount.Text))
            {
                txtLog.Text += "[" + bestCount + "] " + bestName + "\r\n";
                txtLog.Update();

                BBC tmp = coverages[bestName];
                coverages.Remove(bestName);
                results.Add(bestName);
                bestCount = 0;
                foreach (KeyValuePair<string, BBC> entry in coverages)
                {
                    entry.Value.removeBlocks(tmp);
                    if (entry.Value.getCount() > bestCount)
                    {
                        bestCount = entry.Value.getCount();
                        bestName = Path.GetFileName(entry.Key);
                    }
                }
            }

            foreach (string file in results)
            {
                string fileReal = file.Substring(0, file.LastIndexOf("."));
                File.Copy(txtInput.Text + "\\" + fileReal, txtOutput.Text + "\\" + fileReal);
            }
            txtLog.Text += "DONE\r\n";
            btnStart.Enabled = true;
        }
    }
}
