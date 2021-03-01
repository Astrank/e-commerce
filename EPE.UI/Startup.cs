using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EPE.Database;
using EPE.Database.FileManager;
using EPE.Application.Projects;
using Microsoft.AspNetCore.Http;
using EPE.Application.AuthorizedUsers;

namespace EPE.UI
{
    public class Startup
    {
        private IConfiguration _config { get; }

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeFolder("/Admin", "Manager");
                //options.Conventions.AuthorizePage("/Admin");
            });

            /* Defines the db for the app */
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(_config["DefaultConnection"]));
            
            /* Adds identity and password config */
            services.AddIdentity<IdentityUser, IdentityRole>(options => 
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 6;                
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));
                //options.AddPolicy("Manager", policy => policy.RequireClaim("Role", "Manager"));
                options.AddPolicy("Manager", policy => 
                    policy.RequireAssertion(context =>
                        context.User.HasClaim("Role", "Manager") || context.User.HasClaim("Role", "Admin")));
            });

            services.AddTransient<IFileManager, FileManager>();
            services.AddTransient<IProjectRepository, SQLServer_ProjectRepository>();

            services.AddSession(options => 
            {
                options.Cookie.Name = "Cart";
                options.Cookie.MaxAge = TimeSpan.FromMinutes(20);
            });

            services.AddTransient<CreateUser>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseCors("CorsApiPolicy");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern:"{controller=Admin}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

        }
    }
}
