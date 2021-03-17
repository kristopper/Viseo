using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Viseo.Application.Common.Interfaces;
using Viseo.Domain.Common.Enums;
using Viseo.Domain.Entities;

namespace Viseo.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<AppUser> userManager, IApplicationDbContext context)
        {
            var UserRole1 = new AppRole
            {
                AppRoleId = 1,
                Name = "User",
            };


            var UserRole2 = new AppRole
            {
                AppRoleId = 2,
                Name = "Admin",
            };

            await context.AppRoles.AddRangeAsync(new[] { UserRole1, UserRole2 });

            var administrator = new AppUser
            {
                FirstName = "Admin",
                LastName = "Admin",
                Role = Role.Admin,
                Status = Status.Active,
                Email = "Admin@gmail.com",
                UserName = "Admin",
            };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                context.AppUsers.Add(administrator);
                await context.SaveChangesAsync();

                ////Default Admin password failed on validation
                administrator.PasswordHash = userManager.PasswordHasher.HashPassword(administrator, "Password1");
                IdentityResult result = await userManager.UpdateAsync(administrator);
            }
        }
    }
}