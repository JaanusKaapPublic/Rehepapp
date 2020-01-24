using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CoverageTools;

namespace CoverageCommon
{
    public partial class Form1 : Form
    {
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        SaveFileDialog fileSaveDialog = new SaveFileDialog();

        public Form1()
        {
            InitializeComponent();
            fileSaveDialog.DefaultExt = "bbc";
            fileSaveDialog.AddExtension = true;
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

        private async void BtnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            log("STARTING \r\n");
            string[] files = Directory.GetFiles(txtDir.Text, "*.bbc");
            string error;
            if(files.Length == 0)
            {
                MessageBox.Show("No .bbc files");
                return;
            }
            log("[INFO]['" + files[0] + "'] loading\r\n");
            BBC baseObj = new BBC();
            error = baseObj.load(files[0]);
            if(error != null)
            {
                log("[ERROR]['" + files[0] + "'] " + error);
                btnStart.Enabled = true;
                return;
            }
            for (int x = 1; x < files.Length; x++)
            {
                log("[INFO]['" + files[x] + "'] loading\r\n");
                BBC tmpObj = new BBC();
                error = tmpObj.load(files[x]);
                if (error != null)
                {
                    log("[ERROR]['"+ files[x] +"'] " + error);
                    btnStart.Enabled = true;
                    return;
                }
                baseObj.commons(tmpObj);
            }
            baseObj.save(txtOutput.Text);
            log("ALL DONE \r\n");
            btnStart.Enabled = true;
        }

        private void log(string message)
        {
            txtLog.Text += message + "\n";
            txtLog.Update();
        }
    }
}
