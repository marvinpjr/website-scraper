using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;
using Services;
using Domain;
using System.Collections.Generic;

namespace WebsiteContentScraper
{
    [TestClass]
    public class DataContextTests
    {
        [TestMethod]
        public void CanScrape()
        {
            string filename = "testfilename";
            Video testVid = new Video();
            testVid.ShortFileName = filename;
            new FileDownloaderService().DownloadFile(testVid);
        }

        [TestMethod]
        public void CanPreLoadDatabase()
        {
            PageBuilderService pbs = new PageBuilderService();
            pbs.LoadIfEmpty();
        }

        [TestMethod]
        public void CanGetVideoFileNameFromVideoPageContent()
        {
            //TODO Write this
        }

        [TestMethod]
        public void CanGetVideoIds()
        {
            IndexPage ip = new IndexPage
            {
                PageUrl = "http://www.target-site.com/member/index.php?page=viewmodel&id=105",
            };
            ip.Content = new PageContentScraperService().GetContentForPage(ip);
            IEnumerable<int> ids = ip.ParseContentToGetVideoPageIds();

            int count = 0;
            foreach (int id in ids)
                count++;

            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void CanUpdateFileNames()
        {
            var vps = new VideoPageService();
            vps.UpdateVideoFileNamesFromVideoPageContents();
        }

        [TestMethod]
        public void CanDownloadAsync()
        {
            List<Video> videosToDownload = new VideoService().GetVideosWhichHaventBeenDownloadedAndHaveFileName(2);
            new FileDownloaderService().DownloadFiles(videosToDownload);
        }

        [TestMethod]
        public void CanPopulateSubFolderNamesFromVideoPageContent()
        {
            new VideoService().PopulateSubFolderNamesFromVideoPageContent();
            Assert.IsFalse(false);
        }
    }
}
