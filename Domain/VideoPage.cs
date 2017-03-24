
using System;
using System.Text.RegularExpressions;
namespace Domain
{
    public class VideoPage: Page
    {
        public int VideoId { get; set; }
        public Video Video { get; set; }

        public static string GetBaseUrl()
        {
            return "http://www.target-site.com/member/?page=downloadvid&id=";
        }

        //TODO TEST THIS
        public void ParseContentToPopulateVideoFileName()
        {
            string fileName = string.Empty;

            if (!string.IsNullOrEmpty(this.Content))
            {
                Regex regex = new Regex("/[a-z]+[0-9]{0,3}[_]?[a-z]{0,10}[0-9]+.wmv\">Download HD WMV"); 
                Match match = regex.Match(this.Content);

                if (match.Success)
                {
                    int posOfDot = match.Value.IndexOf('.');
                    fileName = match.Value.Substring(1, posOfDot - 1); 
                }
            }

            this.Video.ShortFileName = fileName;
        }
    }
}
