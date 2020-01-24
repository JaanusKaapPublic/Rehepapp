using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Analyzer
{
    public partial class AnalyseProgress : Form
    {
        private IdaHandler idaHandler;
        private List<String> filenames;
        private String baseDir;
        private String output;

        public AnalyseProgress()
        {
            InitializeComponent();
            filenames = new List<string>();
        }

        private bool addFile(List<String> fileList, String filename)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            buff = br.ReadBytes(2);
            br.Close();
            fs.Close();
            if (buff.Length < 2 || buff[0] != 'M' || buff[1] != 'Z')
                return false;
            fileList.Add(filename);
            return true;
        }

        private void addDirectory(List<String> fileList, String dirname)
        {
            Invoke(new Action(() => { txtLog.AppendText("  " + dirname + "\r\n" ); }));

            foreach (string f in Directory.GetFiles(dirname))
            {
                addFile(fileList, f);

            }
            foreach (string d in Directory.GetDirectories(dirname))
            {
                addDirectory(fileList, d);
            }
        }

        public void startAnalysing(String IdaLocation, String baseDirIn, String outputIn)
        {
            idaHandler = new IdaHandler(IdaLocation);
            baseDir = baseDirIn;
            output = outputIn;
            Thread monitor = new Thread(new ThreadStart(start));
            monitor.Start();
        }        
        
        protected void start()
        {
            Invoke(new Action(() => { progress.Text = "SEARCHING ALL THE FILES"; }));
            Invoke(new Action(() => { txtLog.AppendText("SEARCHING THROUGH ALL DIRECTORIES\r\n"); }));
            Invoke(new Action(() => { currentFile.Text = ""; }));
            addDirectory(filenames, baseDir);
            Invoke(new Action(() => { txtLog.AppendText("ALL FILES FOUND\r\n"); }));

            for (int x = 0; x<filenames.Count; x++)
            {
                Invoke(new Action(() => { progress.Text = "ANALYSING " + (x+1) + "/" + filenames.Count; }));
                Invoke(new Action(() => { currentFile.Text = filenames[x]; }));
                Invoke(new Action(() => { txtLog.AppendText("Analysing " + filenames[x] + "\r\n"); }));
                DateTime begin = DateTime.UtcNow;
                idaHandler.startAnalysis(filenames[x], baseDir, output);
                DateTime end = DateTime.UtcNow;
                Invoke(new Action(() => { txtLog.AppendText("  took " + end.Subtract(begin) + " seconds\r\n"); }));
            }
            Invoke(new Action(() => { progress.Text = "DONE"; }));
        }
    }
}
