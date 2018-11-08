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
        }

        public void startAnalysing(String IdaLocation, List<String> filenamesIn, String baseDirIn, String outputIn)
        {
            idaHandler = new IdaHandler(IdaLocation);
            filenames = filenamesIn;
            baseDir = baseDirIn;
            output = outputIn;
            Thread monitor = new Thread(new ThreadStart(start));
            monitor.Start();
        }        
        
        protected void start()
        {
            for(int x = 0; x<filenames.Count; x++)
            {
                Invoke(new Action(() => { progress.Text = "ANALYSING " + (x+1) + "/" + filenames.Count; }));
                Invoke(new Action(() => { currentFile.Text = filenames[x]; }));
                Invoke(new Action(() => { txtLog.AppendText("Analysing file " + filenames[x] + "\r\n"); }));
                DateTime begin = DateTime.UtcNow;
                idaHandler.startAnalysis(filenames[x], baseDir, output);
                DateTime end = DateTime.UtcNow;
                Invoke(new Action(() => { txtLog.AppendText("Analysing took " + end.Subtract(begin) + "\r\n"); }));
            }
            Invoke(new Action(() => { progress.Text = "DONE"; }));
        }
    }
}
