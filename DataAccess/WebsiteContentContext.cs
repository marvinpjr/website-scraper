using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class WebsiteContentContext: DbContext
    {
        public DbSet<IndexPage> IndexPages { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<VideoPage> VideoPages { get; set; }
    }
}
