using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer
{
    class IdaHandler
    {
        private String IdaLocation;

        public IdaHandler(String IdaLocationIn)
        {
            IdaLocation = IdaLocationIn;
        }

        public void startAnalysis(String fileIn, String baseDir, String fileOut)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = IdaLocation;
            startInfo.Arguments = "-A -c -o\"" + Directory.GetCurrentDirectory() + "\\tmp\" -S\"" + Directory.GetCurrentDirectory() + "\\IDA_GetBasicBlocks.py \\\"" + fileOut + "\\\" \\\"" + baseDir + "\\\"\" \"" + fileIn + "\"";
            Process proc = Process.Start(startInfo);
            proc.WaitForExit();
            File.Delete(Directory.GetCurrentDirectory() + "\\tmp.i64");
        }
    }
}
