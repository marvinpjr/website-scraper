using Domain;
using Services.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Services
{
    public class PageContentScraperService
    {
        public string GetContentForPage(Page pageToScrape)
        {
            WebClient wc = new BdWebClientBuilder().GetWebClient();
            byte[] html = wc.DownloadData(pageToScrape.PageUrl);
            return new UTF8Encoding().GetString(html);
        }
    }
}
