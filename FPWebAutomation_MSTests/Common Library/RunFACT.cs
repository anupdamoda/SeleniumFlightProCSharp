using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FPWebAutomation_MSTests.API
{
    public class RunFACT
    {
        public void RunFACTScheduledJob()
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "schtasks /run /s 82-file1 /tn \"FACT\"";
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
