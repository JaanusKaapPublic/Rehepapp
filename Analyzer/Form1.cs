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

        private bool addFile(List<String> fileList,  String filename)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            buff = br.ReadBytes(2);
            br.Close();
            fs.Close();
            if(buff.Length< 2 || buff[0] != 'M' || buff[1] != 'Z')
                return false;
            fileList.Add(filename);
            return true;
        }

        private void addDirectory(List<String> fileList, String dirname)
        {
            foreach (string f in Directory.GetFiles(dirname))
            {
                addFile(fileList, f);

            }
            foreach (string d in Directory.GetDirectories(dirname))
            {
                addDirectory(fileList, d);
            }
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
            List<String> files = new List<String>();
            addDirectory(files, peDir.Text);

            AnalyseProgress progressForm = new AnalyseProgress();
            progressForm.Show(this);
            progressForm.startAnalysing(idaFile.Text, files, peDir.Text, outputFile.Text);
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
            outputFile.Text = Directory.GetCurrentDirectory() + "\\result.codeblocks"; 
        }
    }
}
