using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analyzer
{
    public partial class Form1 : Form
    {
        OpenFileDialog fileDialog = new OpenFileDialog();
        SaveFileDialog fileSaveDialog = new SaveFileDialog();
        FolderBrowserDialog folderDialog = new FolderBrowserDialog();

        public Form1()
        {
            InitializeComponent();
            fileSaveDialog.DefaultExt = "codeblocks";
            fileSaveDialog.AddExtension = true;
            detectDefaults();
        }
        
        private void addDirBtn_Click(object sender, EventArgs e)
        {
            if(folderDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                peDir.Text = folderDialog.SelectedPath;
            }
        }

        private void outputBtn_Click(object sender, EventArgs e)
        {
            if (fileSaveDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                outputFile.Text = fileSaveDialog.FileName;
            }

        }

        private void idaBtn_Click(object sender, EventArgs e)
        {
            if (fileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                idaFile.Text = fileDialog.FileName;
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            AnalyseProgress progressForm = new AnalyseProgress();
            progressForm.Show(this);
            progressForm.startAnalysing(idaFile.Text, peDir.Text, outputFile.Text);
        }

        private void detectDefaults()
        {
            //IDA
            IEnumerable<string> list = Directory.GetDirectories("C:\\Program Files").Where(s => s.Contains("IDA "));
            if(list.Count() > 0)
                idaFile.Text = list.ElementAt(0) + "\\ida64.exe";

            //Input file
            list = Directory.GetDirectories(Directory.GetCurrentDirectory() + "\\").Where(s => s.Contains("files"));
            if (list.Count() > 0)
                peDir.Text = list.ElementAt(0);
            list = Directory.GetDirectories(Directory.GetCurrentDirectory() + "\\").Where(s => s.Contains("input"));
            if (list.Count() > 0)
                peDir.Text = list.ElementAt(0);
            list = Directory.GetDirectories(Directory.GetCurrentDirectory() + "\\").Where(s => s.Contains("target"));
            if (list.Count() > 0)
                peDir.Text = list.ElementAt(0);
            list = Directory.GetDirectories(Directory.GetCurrentDirectory() + "\\").Where(s => s.Contains("bin"));
            if (list.Count() > 0)
                peDir.Text = list.ElementAt(0);

            //Codeblocks
            outputFile.Text = Directory.GetCurrentDirectory() + "\\basicblocks.bbl"; 
        }
    }
}
