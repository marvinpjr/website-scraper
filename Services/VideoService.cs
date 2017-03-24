using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Services
{
    public class VideoService
    {
        private Queries q;

        public VideoService()
        {
            q = new Queries();
        }

        public void PopulateSubFolderNamesFromVideoPageContent()
        {
            List<VideoPage> vps = new VideoPageService().GetVideoPagesWithContent();
            foreach (VideoPage vp in vps)
            {
                Regex r = new Regex("href=\"movies/[A-Za-z0-9_-]{3,30}/");
                Match m = r.Match(vp.Content);
                if (m.Success)
                {
                    Video v = this.GetById(vp.VideoId);
                    if (v != null) v.SubFolderName = m.Value.Substring(13).TrimEnd(new char[] { '/' });
                }
            }
            q.UpdateContext();

        }

        public Video GetById(int VideoId)
        {
            return q.GetVideoById(VideoId);
        }

        public List<Video> GetVideosWhichHaventBeenDownloadedAndHaveFileName(int numFilesToGet = 0)
        {
            List<Video> videos = q.GetVideosThatHaveFileName();

            string dirPath = new FileDownloaderService().GetTargetFolder();

            List<string> fileNamesDownloaded = 
                Directory.EnumerateFiles(dirPath, "*.wmv", SearchOption.AllDirectories).Select(Path.GetFileName).ToList<string>();

            var delta = from v in videos
                        where !(
                            from f in fileNamesDownloaded
                            select f
                        ).Contains(v.ShortFileName + ".wmv")
                        select v;

            if (numFilesToGet > 0)
                return delta.Take(numFilesToGet).ToList<Video>();
            else
                return delta.ToList<Video>();
        }
    }
}
