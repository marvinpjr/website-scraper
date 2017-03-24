using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class PageBuilderService
    {
        private Queries queries;

        public PageBuilderService()
        {
            queries = new Queries();
        }

        public bool LoadIfEmpty()
        {
            IEnumerable<IndexPage> pagesInDb = queries.GetAllIndexPages();
            if (pagesInDb.Count() == 0)
            {
                IEnumerable<IndexPage> indexPagesToAdd = DeriveListOfIndexPages();
                queries.AddWebPages(indexPagesToAdd);

                List<VideoPage> videoPagesToAdd = new List<VideoPage>();
                foreach (IndexPage indexPage in indexPagesToAdd)
                {
                    videoPagesToAdd.AddRange(DeriveListOfVideoPagesForIndexPage(indexPage));
                }
                queries.AddVideoPages(videoPagesToAdd);
                return true;
            }
            return false;
        }

        private IEnumerable<VideoPage> DeriveListOfVideoPagesForIndexPage(IndexPage indexPage)
        {
            PageContentScraperService pcss = new PageContentScraperService();

            indexPage.Content = pcss.GetContentForPage(indexPage);
            IEnumerable<int> videoPageIds = indexPage.ParseContentToGetVideoPageIds();
            foreach (int id in videoPageIds)
            {
                VideoPage pageToAdd = new VideoPage
                {
                    PageUrl = VideoPage.GetBaseUrl() + id.ToString(),
                    qsIdValue = id
                };
                pageToAdd.Content = pcss.GetContentForPage(pageToAdd);
                pageToAdd.Video = new Video();
                pageToAdd.ParseContentToPopulateVideoFileName();

                yield return pageToAdd;                
            }
        }

        private IEnumerable<IndexPage> DeriveListOfIndexPages()
        {
            foreach (int id in IndexPage.GetTargetIds())
            {
                yield return new IndexPage
                {
                    Content = string.Empty,
                    PageUrl = IndexPage.GetBaseUrl() + id.ToString(),
                    qsIdValue = id
                };
            }
        }
    }
}
