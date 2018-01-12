using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteDirectory.Domain;

namespace WebsiteDirectory.Models
{
    public class UrlViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int SelectedPurpose { get; set; }
        public List<Purpose> Purposes { get; set; }
        public int SelectedCompany { get; set; }
        public List<Company> Companies { get; set; }
        public int SelectedDatabase { get; set; }
        public List<Database> Databases { get; set; }
        public int SelectedMode { get; set; }
        public List<Mode> Modes { get; set; }
        public int SelectedWebsite { get; set; }
        public List<Website> Websites { get; set; }
    }
}
