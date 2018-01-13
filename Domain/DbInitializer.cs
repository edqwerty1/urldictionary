using System;
using System.Linq;

namespace WebsiteDirectory.Domain
{
    public static class DbInitializer
    {
        public static void Initialize(WebsiteContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Websites.Any())
            {
                return;   // DB has been seeded
            }

            var websites = new Website[]
            {
                new Website{Name="Retail Site"},
                new Website{Name="Product Admin"}

            };
            foreach (var w in websites)
            {
                context.Websites.Add(w);
            }
            context.SaveChanges();

                        var modes = new Mode[]
            {
                new Mode{Name="Live"},
                new Mode{Name="Local"}

            };
            foreach (var w in modes)
            {
                context.Modes.Add(w);
            }
            context.SaveChanges();


                        var purposes = new Purpose[]
            {
                new Purpose{Name="Live"},
                new Purpose{Name="Local"}

            };
            foreach (var w in purposes)
            {
                context.Purposes.Add(w);
            }
            context.SaveChanges();

                                    var databases = new Database[]
            {
                new Database{Name="Live"},
                new Database{Name="Dev"}

            };
            foreach (var w in databases)
            {
                context.Databases.Add(w);
            }
            context.SaveChanges();

                                    var companies = new Company[]
            {
                new Company{Name="Live"},
                new Company{Name="Local"}

            };
            foreach (var w in companies)
            {
                context.Companies.Add(w);
            }
            context.SaveChanges();
        }
    }
}