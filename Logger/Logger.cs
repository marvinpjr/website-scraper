using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public class Logger
    {
        public void Log(string message)
        {
            using (StreamWriter sw = File.AppendText(@"C:\Users\marvinpjr\Videos\BD\DownloadLogs.txt"))
            {
                sw.Write(message);            
            }
        }
    }
}
