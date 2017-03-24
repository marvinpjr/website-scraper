using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class Queries
    {
        private readonly WebsiteContentContext context;

        public Queries()
        {
            context = new WebsiteContentContext();
        }

        public List<IndexPage> GetAllIndexPages()
        {
            return context.IndexPages.ToList();
        }

        public List<VideoPage> GetAllVideoPages()
        {
            return context.VideoPages.ToList();
        }

        public List<IndexPage> GetUnscrapedWebPages()
        {
            return context.IndexPages.Where(wp => wp.Content.Length <= 0).ToList();
        }

        public void AddWebPage(IndexPage page)
        {
            context.IndexPages.Add(page);
            context.SaveChanges();
        }

        public void AddWebPages(IEnumerable<IndexPage> pages)
        {
            context.Configuration.AutoDetectChangesEnabled = false;
            foreach (IndexPage page in pages)
            {
                context.IndexPages.Add(page);
            }
            context.SaveChanges();
            //context.Dispose();
        }

        public void AddVideo(Video video)
        {
            context.Videos.Add(video);
            context.SaveChanges();
        }

        public void MarkVideoDownloaded(Video video)
        {
            context.Entry(video).Property(v => v.HasBeenDownloaded).CurrentValue = true;
            context.SaveChanges();
        }

        public void AddVideoPages(IEnumerable<VideoPage> pages)
        {
            context.Configuration.AutoDetectChangesEnabled = false;
            foreach (VideoPage page in pages)
            {
                context.VideoPages.Add(page);
            }
            context.SaveChanges();
            //context.Dispose();
        }

        public void UpdateContext()
        {
            context.SaveChanges();
        }

        public List<Video> GetVideosThatHaveFileName()
        {
            return context.Videos.Where(v => !String.IsNullOrEmpty(v.ShortFileName)).ToList();
        }

        public Video GetVideoById(int VideoId)
        {
            return context.Videos.Find(VideoId);
        }
    }
}
