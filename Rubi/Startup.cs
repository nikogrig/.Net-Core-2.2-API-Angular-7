using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Rubi.Data;
using Microsoft.AspNetCore.Identity;
using Rubi.Data.Models;
using Rubi.Infrastructure.ServiceExtension;
using Rubi.Infrastructure.IdentityExtension;
using AutoMapper;
using FluentValidation.AspNetCore;
using Rubi.Infrastructure.DbExtension;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.Cookies;
using Rubi.Services.Db.Contracts;
using Rubi.Services.Db.Implementations;
using Rubi.Filters;
using FluentValidation;
using Rubi.Dtos;
using Rubi.Validators;
using System.Linq;
using MicroElements.Swashbuckle.FluentValidation;

namespace Rubi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation(c =>
                {
                c.RegisterValidatorsFromAssemblyContaining<Startup>();
                }) 
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.OperationFilter<AuthResponsesOperationFilter>();
                c.AddFluentValidationRules();
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddEntityFrameworkNpgsql()
                    .AddDbContext<ApplicationDbContext>()
                    .BuildServiceProvider();

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddDomainServices();

            services.AddAutoMapper(typeof(Startup));
            //services.AddAutoMapper(Type assemblyTypeToSearch);
            //services.AddAutoMapper(params Type[] assemblyTypesToSearch);

            //services.AddTransient<IValidator<RegisterFormDto>, RegisterFormValidator>();
            var serviceDescriptors = services.Where(descriptor => descriptor.ServiceType.GetInterfaces().Contains(typeof(IValidator))).ToList();
            serviceDescriptors.ForEach(descriptor => services.Add(ServiceDescriptor.Transient(typeof(IValidator), descriptor.ImplementationType)));

            services
                .AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.SaveToken = true;
                    cfg.RequireHttpsMetadata = false;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        //ValidAudience = Configuration["JwtIssuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JwtKey"])),
                    };
                });

            services.AddScoped<IDbInitializer, DbInitializer>();

            // In production, the Angular files will be served from this directory  ss
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "src/client/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbInitializer dbInitializer)
        {
            //app.UseDatabaseMigrationWithIdentities();
            //app.UseDatabaseMigration();

            app.UseMvc()
               //.UseScopedSwagger();
               .UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseSpaStaticFiles();

            app.UseAuthentication();

            dbInitializer.Initialize();

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "src/client";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
