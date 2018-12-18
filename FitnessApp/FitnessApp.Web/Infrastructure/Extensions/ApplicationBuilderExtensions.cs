namespace FitnessApp.Web.Infrastructure.Extensions
{
    using Common.Constants;
    using Data;
    using FitnessApp.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        public static void UpdateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<FitnessDbContext>();
                context.Database.Migrate();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<FitnessUser>>();

                Task.Run(async () =>
                {
                    var adminName = RolesConstants.ADMINISTRATOR_ROLE;

                    var exists = await roleManager.RoleExistsAsync(adminName);

                    if (!exists)
                    {
                        await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = adminName
                        });
                    }

                    var adminUser = await userManager.FindByNameAsync(adminName);

                    if (adminUser == null)
                    {
                        adminUser = new FitnessUser
                        {
                            UserName = "admin",
                            Email = "admin@admin.com"
                        };

                        await userManager.CreateAsync(adminUser, "admin12");
                        await userManager.AddToRoleAsync(adminUser, adminName);
                    }

                })
                .GetAwaiter()
                .GetResult();
            }
        }
    }
}
