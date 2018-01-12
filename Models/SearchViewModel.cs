using System.Collections.Generic;

namespace WebsiteDirectory.Models
{
    public class SearchViewModel
    {
        public List<SearchItem> Sites { get; set; }
        public List<SearchItem> Modes { get; set; }
        public List<SearchItem> Databases { get; set; }
        public List<SearchItem> Companies { get; set; }
        public List<SearchItem> Purposes { get; set; }
    }

    public class SearchItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}
