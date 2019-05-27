using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rubi.Data;
using Rubi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Rubi.Infrastructure.IdentityExtension
{
    public static class IdentitiesBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigrationWithIdentities(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope
                    .ServiceProvider
                    .GetService<ApplicationDbContext>();

                context.Database
                       .Migrate();

                var userManager = serviceScope
                     .ServiceProvider
                     .GetService<UserManager<ApplicationUser>>();

                var roleManager = serviceScope
                    .ServiceProvider
                    .GetService<RoleManager<IdentityRole>>();

                Task
                    .Run(async () =>
                    {
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
                                await roleManager.CreateAsync(new IdentityRole
                                {
                                    Name = role
                                });
                            }
                        }

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
                    })
                .Wait();
            }

            return app;
        }
    }
}

