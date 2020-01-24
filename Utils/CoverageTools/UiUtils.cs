using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoverageTools
{
    class UiUtils
    {
        static public string getExistingDirectory(string[] dirs)
        {
            string curDir = Directory.GetCurrentDirectory() + "\\";

            foreach (string dir in dirs)
                if (Directory.Exists(curDir + dir))
                    return curDir + dir;

            return "";
        }
        static public string getExistingFile(string[] files)
        {
            string curDir = Directory.GetCurrentDirectory();

            foreach (string file in files)
            {
                string[] results = Directory.GetFiles(curDir, file);
                if (results.Length > 0)
                    return results[0];
            }

            return "";
        }
    }
}
