using System.Collections.Generic;

namespace WebsiteDirectory.Models
{
    public class SitesListViewModel
    {
        public List<SitesListDetails> Sites { get; set; }
    }

    public class SitesListDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Website { get; set; }
        public string Mode { get; set; }
        public string Database { get; set; }
        public string Company { get; set; }
        public string Purpose { get; set; }
    }
}
