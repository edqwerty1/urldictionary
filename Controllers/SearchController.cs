using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebsiteDirectory.Domain;
using WebsiteDirectory.Models;

namespace website_directory.Controllers
{
    [Route("api/[controller]")]
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
                Companies = database.Companies.OrderBy(t => t.Name).Select(t => new SearchItem { Name = t.Name, Id = t.Id, Selected = true }).ToList(),
                Databases = database.Databases.OrderBy(t => t.Name).Select(t => new SearchItem { Name = t.Name, Id = t.Id,Selected = true }).ToList(),
                Modes = database.Modes.OrderBy(t => t.Name).Select(t => new SearchItem { Name = t.Name, Id = t.Id, Selected = true }).ToList(),
                Purposes = database.Purposes.OrderBy(t => t.Name).Select(t => new SearchItem { Name = t.Name, Id = t.Id, Selected = true }).ToList(),
                Websites = database.Websites.OrderBy(t => t.Name).Select(t => new SearchItem { Name = t.Name, Id = t.Id, Selected = true }).ToList()
            };
        }

        [HttpPost("[action]")]
        public SitesListViewModel GetUrls([FromBody]SearchViewModel model)
        {
            return new SitesListViewModel
            {
                Sites = database.WebsiteUrls
                .Where(url => model.Companies.Where(t => t.Selected).Any(c => c.Id == url.Company.Id)
                           && model.Databases.Where(t => t.Selected).Any(c => c.Id == url.Database.Id)
                           && model.Modes.Where(t => t.Selected).Any(c => c.Id == url.Mode.Id)
                           && model.Purposes.Where(t => t.Selected).Any(c => c.Id == url.Purpose.Id)
                           && model.Websites.Where(t => t.Selected).Any(c => c.Id == url.Website.Id)
                           )
                .Select(url => new SitesListDetails
                {
                    Id = url.Id,
                    Company = url.Company.Name,
                    Name = url.Name,
                    Database = url.Database.Name,
                    Mode = url.Mode.Name,
                    Purpose = url.Purpose.Name,
                    Website = url.Website.Name,
                    Url = url.Url
                })
                .ToList()
            };
        }

                [HttpGet("[action]")]
        public List<ProfileViewModel> GetProfiles()
        {
            return database.Profiles.OrderBy(t => t.Name).Select(t => new ProfileViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Companies = t.Companies.Select(c => c.Company).ToList(),
                Purposes = t.Purposes.Select(c => c.Purpose).ToList(),
                Websites = t.Websites.Select(c => c.Website).ToList(),
                Databases = t.Databases.Select(c => c.Database).ToList(),
                Modes = t.Modes.Select(c => c.Mode).ToList()
            }).ToList();
        }

        public class ProfileViewModel
        {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Purpose> Purposes { get; set; }
        public List<Company> Companies { get; set; }
        public List<Database> Databases { get; set; }
        public List<Mode> Modes { get; set; }
        public List<Website> Websites { get; set; }
        }

                        [HttpPost("[action]")]
        public IActionResult AddProfile([FromBody]ProfileViewModel model)
        {
            if (database.Profiles.Any(t => t.Name == model.Name))
                return BadRequest();

            var profile = new Profile{
                Name = model.Name
            };
            profile.Companies = database.Companies.Where(db => model.Companies.Any(m => m.Id == db.Id)).Select(t => new CompanyProfile { Company = t, Profile = profile}).ToList();
            profile.Databases = database.Databases.Where(db => model.Databases.Any(m => m.Id == db.Id)).Select(t => new DatabaseProfile { Database = t, Profile = profile }).ToList();
            profile.Purposes = database.Purposes.Where(db => model.Purposes.Any(m => m.Id == db.Id)).Select(t => new PurposeProfile { Purpose = t, Profile = profile }).ToList();
            profile.Websites = database.Websites.Where(db => model.Websites.Any(m => m.Id == db.Id)).Select(t => new WebsiteProfile { Website = t, Profile = profile }).ToList();
            profile.Modes = database.Modes.Where(db => model.Modes.Any(m => m.Id == db.Id)).Select(t => new ModeProfile { Mode = t, Profile = profile }).ToList();

            database.Profiles.Add(profile);
            database.SaveChanges();
            return Ok();
        }

                                [HttpGet("[action]/{profileName}")]
        public ProfileViewModel GetProfile(string profileName)
        {
            return database.Profiles.Select(t => new ProfileViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Companies = t.Companies.Select(c => c.Company).ToList(),
                Purposes = t.Purposes.Select(c => c.Purpose).ToList(),
                Websites = t.Websites.Select(c => c.Website).ToList(),
                Databases = t.Databases.Select(c => c.Database).ToList(),
                Modes = t.Modes.Select(c => c.Mode).ToList()
            }).First(t => t.Name.ToLower() == profileName.ToLower());
        }
    }
}