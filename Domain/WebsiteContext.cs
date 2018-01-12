using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteDirectory.Domain
{
    public class WebsiteContext : DbContext
    {
        public WebsiteContext(DbContextOptions<WebsiteContext> options)
            : base(options)
        { }

        public DbSet<Website> Websites { get; set; }
        public DbSet<Mode> Modes { get; set; }
        public DbSet<Database> Databases { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Purpose> Purposes { get; set; }
        public DbSet<WebsiteUrl> WebsiteUrls { get; set; }
        public DbSet<Profile> Profiles { get; set; }
    }

    public class Website
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<WebsiteUrl> Websites { get; set; }
    }

    public class Mode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<WebsiteUrl> Websites { get; set; }
    }

    public class Database
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<WebsiteUrl> Websites { get; set; }
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<WebsiteUrl> Websites { get; set; }
    }

    public class Purpose
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<WebsiteUrl> Websites { get; set; }
    }

    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Purpose> Purposes { get; set; }
        public List<Company> Companies { get; set; }
        public List<Database> Databases { get; set; }
        public List<Mode> Modes { get; set; }
        public List<Website> Websites { get; set; }
    }

    public class WebsiteUrl
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Purpose Purpose { get; set; }
        public Company Company { get; set; }
        public Database Database { get; set; }
        public Mode Mode { get; set; }
        public Website Website { get; set; }
    }
}
