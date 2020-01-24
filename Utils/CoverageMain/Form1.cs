using CoverageTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoverageMain
{
    public partial class Form1 : Form
    {
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        OpenFileDialog fileLoadDialog = new OpenFileDialog();

        public Form1()
        {
            InitializeComponent(); 
            txtBBL.Text = UiUtils.getExistingFile(new string[] { "*.bbl", "*.codeblocks" });
            txtDoneDir.Text = UiUtils.getExistingDirectory(new string[] { "done" });
            txtInputDir.Text = UiUtils.getExistingDirectory(new string[] { "input", "files" });
            txtOutputDir.Text = UiUtils.getExistingDirectory(new string[] { "out", "output", "results", "result", "cov", "coverage"});

            if(!File.Exists("tracer.exe"))
            {
                MessageBox.Show("There is no \"tracer.exe\" file in same directory as this program. You can't record code coverage without it!");
            }
        }

        private void BtnBBL_Click(object sender, EventArgs e)
        {
            if (fileLoadDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtBBL.Text = fileLoadDialog.FileName;
            }
        }

        private void BtnExec_Click(object sender, EventArgs e)
        {
            if (fileLoadDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtExec.Text = fileLoadDialog.FileName;
                txtBaseDir.Text = txtExec.Text.Substring(0, txtExec.Text.LastIndexOf("\\"));
            }
        }

        private void BtnBaseDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtBaseDir.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void BtnInputDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtInputDir.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void BtnDoneDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtDoneDir.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void BtnOutputDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtOutputDir.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private async void BtnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            txtLog.Text += "STARTING\r\n";
            txtLog.Update();
            string[] files = Directory.GetFiles(txtInputDir.Text, "*");
            string[] preCmds = txtPreCmd.Text.Split('\n');
            foreach (string file in files)
            {
                foreach (string preCmd in preCmds)
                {
                    string strCmdText = "/C " + preCmd.Trim();
                    Process p = System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                    p.WaitForExit();
                }

                txtLog.Text += file + "\r\n";
                txtLog.Update();
                Process process = new Process();
                string argument = txtCmd.Text.Replace("%INPUT%", file);
                process.StartInfo.FileName = "tracer.exe";
                process.StartInfo.Arguments = "\"" + txtBBL.Text + "\" \"" +
                    txtTime.Text + "\" \"" +
                    txtOutputDir.Text + "\\" + Path.GetFileName(file) + ".bbc\" \"" +
                    txtBaseDir.Text + "\" \"" +
                    txtExec.Text + "\" " +
                    argument;
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                process.Start();
                if (!process.WaitForExit(1000 * 60 * 4))
                    process.Kill();
                while (true)
                {
                    try
                    {
                        File.Move(file, txtDoneDir.Text + "\\" + Path.GetFileName(file));
                        break;
                    }
                    catch(Exception ex)
                    {
                        txtLog.Text += "[WARNING][MOVE-FAILED][RETRYING]" + file + "\r\n";
                        txtLog.Update();
                        Thread.Sleep(1000);
                    }
                }
            }
            txtLog.Text += "ALL DONE\r\n";
            btnStart.Enabled = true;
        }
    }
}