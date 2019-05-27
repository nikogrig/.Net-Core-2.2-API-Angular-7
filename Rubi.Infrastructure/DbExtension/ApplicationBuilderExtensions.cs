using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rubi.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rubi.Infrastructure.DbExtension
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app
                                         .ApplicationServices
                                         .GetRequiredService<IServiceScopeFactory>()
                                         .CreateScope())
            {
                serviceScope
                    .ServiceProvider
                    .GetService<ApplicationDbContext>()
                    .Database
                    .Migrate();
            }

            return app;
        }
    }
}
