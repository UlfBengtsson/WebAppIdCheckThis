using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppIdCheck.Models;

namespace WebAppIdCheck.Data
{
    public static class DbInitializer
    {
        internal static void Initialize(ApplicationDbContext context)
        {
            // Check if our database is created if not then create it
            context.Database.EnsureCreated();

            // Look for any dogs.
            if (context.Dogs.Any())
            {
                return;   // DB has been seeded
            }

            var dogSeed = new Dog[]
            {
                new Dog() { Name = "Pluto", Breed="Disny bloodhound" },
                new Dog() { Name = "Tigger", Breed="Bloodhound" },
                new Dog() { Name = "Keon", Breed="Irish wolfhound" },
                new Dog() { Name = "ZEUS", Breed="Great Dane" },

            };
            foreach (Dog dog in dogSeed)
            {
                context.Dogs.Add(dog);
            }
            context.SaveChanges();
        }
    }
}
