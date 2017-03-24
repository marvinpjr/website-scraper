using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class VideoPageService
    {
        Queries q;

        public VideoPageService()
        {
            q = new Queries();
        }

        public List<VideoPage> GetVideoPagesWithContent()
        {
            return q.GetAllVideoPages();
        }

        public void UpdateVideoFileNamesFromVideoPageContents()
        {
            List<VideoPage> pages = GetVideoPagesWithContent();
            foreach (VideoPage page in pages)
            {
                page.ParseContentToPopulateVideoFileName();
                q.UpdateContext();
            }
        }
    }
}
