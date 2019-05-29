using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rubi.Data;
using Rubi.Data.Models;
using Rubi.Services.Db.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rubi.Services.Db.Implementations
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceProvider serviceProvider;
        public DbInitializer(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async void Initialize()
        {
            using (var serviceScope = this.serviceProvider
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                //create database schema if none exists
                var context = serviceScope
                    .ServiceProvider
                    .GetService<ApplicationDbContext>();

                context.Database.Migrate();

                var userManager = serviceScope
                    .ServiceProvider
                    .GetService<UserManager<ApplicationUser>>();

                //If there is already an Administrator role, abort
                var roleManager = serviceScope
                    .ServiceProvider
                    .GetService<RoleManager<ApplicationRole>>();

                var adminRoleName = "Administrator";     //todo

                var roles = new[]
                {
                            adminRoleName,
                            "Customer"  // TODO
                        };

                foreach (var role in roles)
                {
                    bool roleExists = await roleManager.RoleExistsAsync(role);

                    if (!roleExists)
                    {
                        await roleManager.CreateAsync(new ApplicationRole
                        {
                            Name = role
                        });
                    }
                }

                //Create the default Admin account and apply the Administrator role

                var adminEmail = "admin@ruby.net";

                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                if (adminUser == null)
                {
                    adminUser = new ApplicationUser
                    {
                        UserName = "RubyAdmin",
                        Email = adminEmail,
                        //ProfilePicture = new byte[0],
                        FirstName = "Nikola",
                        LastName = "Grigorov",
                        Address = "Sofia",
                        Birthdate = Convert.ToDateTime("1986/06/03"),
                        //AppRoleID = new Guid().ToString(),
                        PhoneNumber = "+0888123456"
                    };

                    await userManager.CreateAsync(adminUser, "admin123");

                     await userManager.AddToRoleAsync(adminUser, adminRoleName);
                }

                //string user = "RubyAdmin";

                //var success = await userManager
                //    .CreateAsync(new ApplicationUser
                //    {
                //        UserName = user,
                //        Email = "admin@ruby.net",
                //        FirstName = "Nikola",
                //        LastName = "Grigorov",
                //        Address = "Sofia",
                //        Birthdate = Convert.ToDateTime("1986/06/03"),
                //        PhoneNumber = "+0888123456"
                //    }, "admin123");

                //if (success.Succeeded)
                //{
                //    await userManager.AddToRoleAsync(await userManager.FindByNameAsync(user), "Administrator");
                //}
            }
        }
    }
}
