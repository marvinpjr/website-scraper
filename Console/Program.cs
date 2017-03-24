using Domain;
using Logging;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Video> videosToDownload;
            if (args.Length == 0)
            {
                videosToDownload = new VideoService().GetVideosWhichHaventBeenDownloadedAndHaveFileName(1);
            }
            else
            {
                int numVids = int.Parse(args[0]);
                videosToDownload = new VideoService().GetVideosWhichHaventBeenDownloadedAndHaveFileName(numVids);
            }

            int count = 1;

            StringBuilder sbLog = new StringBuilder();
            foreach (Video video in videosToDownload)
            {
                sbLog.Append(String.Format("{0}/{1}. Starting download of '{2}' at {3}....",
                    count.ToString(),
                    videosToDownload.Count.ToString(),
                    video.ShortFileName, 
                    DateTime.Now.ToShortTimeString()
                    ));

                sbLog.Append(Environment.NewLine);
                try { new FileDownloaderService().DownloadFiles(videosToDownload); }
                catch (Exception ex) {
                    sbLog.Append(String.Format("The following exception occurred: {0}", ex.Message));
                    sbLog.Append(String.Format("Stack: {0}", ex.StackTrace));
                    sbLog.Append(Environment.NewLine);
                }
                sbLog.Append(String.Format("Finished at {0}", DateTime.Now.ToShortTimeString()));
                sbLog.Append(Environment.NewLine);
                count++;
            }
            new Logger().Log(sbLog.ToString());
        }
    }
}
