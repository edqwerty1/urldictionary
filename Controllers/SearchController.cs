using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebsiteDirectory.Domain;
using WebsiteDirectory.Models;

namespace website_directory.Controllers
{
    public class SearchController : Controller
    {
        private readonly WebsiteContext database;
        public SearchController(WebsiteContext database)
        {
            this.database = database;
        }

        [HttpGet("[action]")]
        public SearchViewModel GetOptions()
        {
            return new SearchViewModel
            {
                Companies = database.Companies.OrderBy(t => t.Name).Select(t => new SearchItem { Name = t.Name, Id = t.Id }).ToList(),
                Databases = database.Databases.OrderBy(t => t.Name).Select(t => new SearchItem { Name = t.Name, Id = t.Id }).ToList(),
                Modes = database.Modes.OrderBy(t => t.Name).Select(t => new SearchItem { Name = t.Name, Id = t.Id }).ToList(),
                Purposes = database.Purposes.OrderBy(t => t.Name).Select(t => new SearchItem { Name = t.Name, Id = t.Id }).ToList(),
                Sites = database.Websites.OrderBy(t => t.Name).Select(t => new SearchItem { Name = t.Name, Id = t.Id }).ToList()
            };
        }

        [HttpPost("[action]")]
        public SitesListViewModel GetUrls(SearchViewModel model)
        {
            return new SitesListViewModel
            {
                Sites = database.WebsiteUrls
                .Where(url => model.Companies.Where(t => t.Selected).Any(c => c.Id == url.Company.Id)
                           && model.Databases.Where(t => t.Selected).Any(c => c.Id == url.Database.Id)
                           && model.Modes.Where(t => t.Selected).Any(c => c.Id == url.Mode.Id)
                           && model.Purposes.Where(t => t.Selected).Any(c => c.Id == url.Purpose.Id)
                           && model.Sites.Where(t => t.Selected).Any(c => c.Id == url.Website.Id)
                           )
                .Select(url => new SitesListDetails
                {
                    Id = url.Id,
                    Company = url.Company.Name,
                    Name = url.Name,
                    Database = url.Database.Name,
                    Mode = url.Mode.Name,
                    Purpose = url.Purpose.Name,
                    Url = url.Url
                })
                .ToList()
            };
        }
    }
}