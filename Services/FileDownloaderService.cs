using System;
using System.Collections.Generic;
using Domain;
using Services.Internal;
using System.IO;
using System.Net;

namespace Services
{
    public class FileDownloaderService
    {
        private Queue<Video> _videosToDownload;

        public FileDownloaderService()
        {
            _videosToDownload = new Queue<Video>();
        }

        public void DownloadFile(Video video) // string fileUri, string fileNameNoExtension
        {
            // try async like this:
            // http://stackoverflow.com/questions/6992553/how-do-i-async-download-multiple-files-using-webclient-but-one-at-a-time/6992743#6992743

            WebClient wc = new BdWebClientBuilder().GetWebClient();
            string targetPath = GetTargetFolder() + video.ShortFileName + ".wmv";
            if (!File.Exists(targetPath)) wc.DownloadFile(video.FileUrl, targetPath);
        }

        public void DownloadFiles(List<Video> videos)
        {
            foreach (Video video in videos)
            {
                DownloadFile(video);
            }
        }

        public void DownloadFilesAsync(List<Video> videos)
        {
            foreach (Video video in videos)
            {
                _videosToDownload.Enqueue(video);
            }

            DownloadNext();
        }

        private void DownloadNext()
        {
            if (_videosToDownload.Count > 0)
            {
                WebClient wc = new BdWebClientBuilder().GetWebClient();
                wc.DownloadFileCompleted += wc_DownloadFileCompleted;

                Video videoToDownload = _videosToDownload.Dequeue();

                string targetPath = GetTargetFolder() + videoToDownload.ShortFileName + ".wmv";
                wc.DownloadFileAsync(new Uri(videoToDownload.FileUrl), targetPath);
            }
        }

        void wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

            DownloadNext();
        }

        public string GetTargetFolder()
        {
            return "C:\\Users\\marvinpjr\\Videos\\BD\\";
        }
    }
}
