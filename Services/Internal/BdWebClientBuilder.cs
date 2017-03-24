using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Services.Internal
{
    internal class BdWebClientBuilder
    {
        public WebClient GetWebClient()
        {
            return GetClientForWebsiteContentWebRequest();
        }

        private WebClient GetClientForWebsiteContentWebRequest()
        {
            WebClient wc = new WebClient();
            wc.Headers = getWebHeaders();
            return wc;
        }

        private WebHeaderCollection getWebHeaders()
        {
            WebHeaderCollection collection = new WebHeaderCollection();
            collection.Add("Authorization", "Basic bWFydmlucGpyOlNsaXBwZXJ5");
            collection.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.57 Safari/537.17");
            collection.Add("Accept-Encoding", "gzip,deflate,sdch");
            collection.Add("Accept-Language", "en,en-US;q=0.8");
            collection.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.3");
            collection.Add("Cookie", "__utma=172058544.1288048630.1359786408.1360589670.1360644201.9; __utmb=172058544.24.10.1360644201; __utmc=172058544; __utmz=172058544.1359786408.1.1.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided)");
            return collection;
        }
    }
}
