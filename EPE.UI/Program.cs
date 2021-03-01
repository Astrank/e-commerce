using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using EPE.Database;
using System;
using System.Security.Claims;

namespace EPE.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            try
            {
                var scope = host.Services.CreateScope();

                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                //var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                context.Database.EnsureCreated();

                /*var adminRole = new IdentityRole("Admin");
                if (!context.Roles.Any())
                {
                    roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
                }*/

                if (!context.Users.Any())
                {
                    var adminUser = new IdentityUser
                    {
                        UserName = "admin",
                        Email = "admin@asd.com"
                    };
                    var managerUser = new IdentityUser
                    {
                        UserName = "manager",
                        Email = "manager@asd.com"
                    };
                    
                    userMgr.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
                    userMgr.CreateAsync(managerUser, "password").GetAwaiter().GetResult();

                    var adminClaim = new Claim("Role", "Admin");
                    var managerClaim = new Claim("Role", "Manager");

                    userMgr.AddClaimAsync(adminUser, adminClaim).GetAwaiter().GetResult();
                    userMgr.AddClaimAsync(managerUser, managerClaim).GetAwaiter().GetResult();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
