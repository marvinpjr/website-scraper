using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Domain
{
    public class IndexPage: Page
    {
        public virtual List<VideoPage> VideoPages { get; set; }

        public static string GetBaseUrl()
        {
            return "http://www.target-site.com/member/index.php?page=viewmodel&id=";
        }
        public static IEnumerable<int> GetTargetIds()
        {
            return new List<int> { 62, 66, 73, 77, 81, 82, 83, 84, 85, 86, 88, 89, 90, 92, 95, 97, 99, 100, 101, 105, 106, 108 };
        }

        public IEnumerable<int> ParseContentToGetVideoPageIds()
        {
            List<int> listToReturn = new List<int>();

            if (!string.IsNullOrEmpty(this.Content))
            {
                Regex regex = new Regex(@"\?page=downloadvid\&id=\d+");
                MatchCollection matches = regex.Matches(this.Content);

                foreach (Match match in matches)
                {
                    int posOfLastEquals = match.Value.LastIndexOf('=');
                    int id = Convert.ToInt32(match.Value.Substring(posOfLastEquals+1));
                    listToReturn.Add(id);
                }
            }

            return listToReturn;
        }
    }
}
