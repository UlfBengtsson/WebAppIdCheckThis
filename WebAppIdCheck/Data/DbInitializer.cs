using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppIdCheck.Models;

namespace WebAppIdCheck.Data
{
    public static class DbInitializer
    {
        internal static void Initialize(
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager
            )
        {
            // Check if our database is created if not then create it
            context.Database.EnsureCreated();

            // Look for any dogs.
            if (context.Roles.Any())
            {
                return;   // DB has been seeded
            }

            //-------------- Roles seed --------------------------------------//

            IdentityRole[] identityRoles = new IdentityRole[]
            {
                new IdentityRole("Admin"),
                new IdentityRole("Guru")
            };
            foreach (var role in identityRoles)
            {
                roleManager.CreateAsync(role).Wait();
            }

            //-------------- Users seed --------------------------------------//

            IdentityUser[] identityUsers = new IdentityUser[]
            {
                new IdentityUser("Admin"),
                new IdentityUser("Guru")
            };

            identityUsers[0].Email = "a@a.a";
            identityUsers[1].Email = "g@g.g";

            string[] passwords = { "Qwerty!23456", "Password!23456" };

            for (int i = 0; i < identityUsers.Length; i++)
            {
                userManager.CreateAsync(identityUsers[i], passwords[i]);
            }

            //--------------Role to Users seed --------------------------------------//



            //-------------- Dogs seed --------------------------------------//

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
