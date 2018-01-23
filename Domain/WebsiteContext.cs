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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WebsiteProfile>()
                .HasKey(t => new { t.WebsiteId, t.ProfileId });

            modelBuilder.Entity<WebsiteProfile>()
                .HasOne(pt => pt.Profile)
                .WithMany(p => p.Websites)
                .HasForeignKey(pt => pt.ProfileId);

            modelBuilder.Entity<WebsiteProfile>()
                .HasOne(pt => pt.Website)
                .WithMany()
                .HasForeignKey(pt => pt.WebsiteId);

            modelBuilder.Entity<ModeProfile>()
    .HasKey(t => new { t.ModeId, t.ProfileId });

            modelBuilder.Entity<ModeProfile>()
                .HasOne(pt => pt.Profile)
                .WithMany(p => p.Modes)
                .HasForeignKey(pt => pt.ProfileId);

            modelBuilder.Entity<ModeProfile>()
                .HasOne(pt => pt.Mode)
                .WithMany()
                .HasForeignKey(pt => pt.ModeId);

            modelBuilder.Entity<DatabaseProfile>()
    .HasKey(t => new { t.DatabaseId, t.ProfileId });

            modelBuilder.Entity<DatabaseProfile>()
                .HasOne(pt => pt.Profile)
                .WithMany(p => p.Databases)
                .HasForeignKey(pt => pt.ProfileId);

            modelBuilder.Entity<DatabaseProfile>()
                .HasOne(pt => pt.Database)
                .WithMany()
                .HasForeignKey(pt => pt.DatabaseId);

            modelBuilder.Entity<CompanyProfile>()
    .HasKey(t => new { t.CompanyId, t.ProfileId });

            modelBuilder.Entity<CompanyProfile>()
                .HasOne(pt => pt.Profile)
                .WithMany(p => p.Companies)
                .HasForeignKey(pt => pt.ProfileId);

            modelBuilder.Entity<CompanyProfile>()
                .HasOne(pt => pt.Company)
                .WithMany()
                .HasForeignKey(pt => pt.CompanyId);

            modelBuilder.Entity<PurposeProfile>()
    .HasKey(t => new { t.PurposeId, t.ProfileId });

            modelBuilder.Entity<PurposeProfile>()
                .HasOne(pt => pt.Profile)
                .WithMany(p => p.Purposes)
                .HasForeignKey(pt => pt.ProfileId);

            modelBuilder.Entity<PurposeProfile>()
                .HasOne(pt => pt.Purpose)
                .WithMany()
                .HasForeignKey(pt => pt.PurposeId);
        }
    }

    public class Website
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<WebsiteUrl> Websites { get; set; }
    }

    public class WebsiteProfile
    {
        public int WebsiteId { get; set; }
        public Website Website { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }

    public class Mode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<WebsiteUrl> Websites { get; set; }
    }

    public class ModeProfile
    {
        public int ModeId { get; set; }
        public Mode Mode { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }

    public class Database
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<WebsiteUrl> Websites { get; set; }
    }

    public class DatabaseProfile
    {
        public int DatabaseId { get; set; }
        public Database Database { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<WebsiteUrl> Websites { get; set; }
    }

    public class CompanyProfile
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }

    public class Purpose
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<WebsiteUrl> Websites { get; set; }
    }

    public class PurposeProfile
    {
        public int PurposeId { get; set; }
        public Purpose Purpose { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }

    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PurposeProfile> Purposes { get; set; }
        public List<CompanyProfile> Companies { get; set; }
        public List<DatabaseProfile> Databases { get; set; }
        public List<ModeProfile> Modes { get; set; }
        public List<WebsiteProfile> Websites { get; set; }
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
