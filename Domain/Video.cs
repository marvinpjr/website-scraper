using System.Text.RegularExpressions;

namespace Domain
{
    public class Video
    {
        public int Id { get; set; }
        public string SubFolderName { get; set; }
        public string ShortFileName { get; set; }
        public bool HasBeenDownloaded { get; set; }

        public string FileUrl { get { return GetBaseUrl() + SubFolderName + "/" + ShortFileName + ".wmv"; }}

        private string GetBaseUrl()
        {
            return "http://www.target-site.com/member/movies/";
        }
    }
}
