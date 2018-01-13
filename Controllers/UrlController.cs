using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebsiteDirectory.Domain;
using WebsiteDirectory.Models;

namespace website_directory.Controllers
{
    [Route("api/[controller]")]
    public class UrlController : Controller
    {
        private readonly WebsiteContext database;
        public UrlController(WebsiteContext database)
        {
            this.database = database;
        }

        [HttpGet("[action]")]
        public UrlViewModel GetUrl(int id)
        {
            var url = database.WebsiteUrls.First(t => t.Id == id);
            var viewModel = new UrlViewModel
            {
                Url = url.Url,
                Id = url.Id,
                Name = url.Name,
                SelectedCompany = url.Company.Id,
                SelectedDatabase = url.Database.Id,
                SelectedMode = url.Mode.Id,
                SelectedPurpose = url.Purpose.Id,
                SelectedWebsite = url.Website.Id,
                Companies = database.Companies.OrderBy(t => t.Name).ToList(),
                Databases = database.Databases.OrderBy(t => t.Name).ToList(),
                Modes = database.Modes.OrderBy(t => t.Name).ToList(),
                Purposes = database.Purposes.OrderBy(t => t.Name).ToList(),
                Websites = database.Websites.OrderBy(t => t.Name).ToList(),
            };
            return viewModel;
        }

        [HttpPost("[action]")]
        public IActionResult AddUrl([FromBody]UrlViewModel model)
        {
            database.WebsiteUrls.Add(new WebsiteUrl
            {
                Company = database.Companies.First(c => c.Id == model.SelectedCompany),
                Database = database.Databases.First(c => c.Id == model.SelectedCompany),
                Mode = database.Modes.First(c => c.Id == model.SelectedCompany),
                Purpose = database.Purposes.First(c => c.Id == model.SelectedCompany),
                Website = database.Websites.First(c => c.Id == model.SelectedCompany),
                Url = model.Url,
                Name = model.Name
            });

            database.SaveChanges();
            return Ok();
        }

        [HttpPut("{urlId:int}/[action]")]
        public IActionResult EditUrl(int urlId, UrlViewModel model)
        {
            var url = database.WebsiteUrls.First(t => t.Id == urlId);

            url.Company = database.Companies.First(c => c.Id == model.SelectedCompany);
            url.Database = database.Databases.First(c => c.Id == model.SelectedCompany);
            url.Mode = database.Modes.First(c => c.Id == model.SelectedCompany);
            url.Purpose = database.Purposes.First(c => c.Id == model.SelectedCompany);
            url.Website = database.Websites.First(c => c.Id == model.SelectedCompany);
            url.Name = model.Name;
            url.Url = model.Url;

            database.SaveChanges();
            return null;
        }
    }
}