using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Moq;
using Viseo.Application.Common.Interfaces;
using Viseo.Application.Services;
using Viseo.Domain.Common.Enums;
using Viseo.Domain.Entities;
using Viseo.Persistence;

namespace Viseo.Application.UnitTests.Common
{
    public class DataSeeder
    {
        //public async static void SeedUsers(UserManager<AppUser> userManager, ApplicationDbContext context, Mock<ICurrentUserService> _mockCurrentUser)
        public async static void SeedUsers(UserManager<AppUser> userManager, ApplicationDbContext context, Mock<ICurrentUserService> _mockCurrentUser)
        {
            _mockCurrentUser.Setup(s => s.Id).Returns(Constant.AdminId);
            
            var userRole = new AppRole
            {
                AppRoleId = (int)Role.user,
                Name = "User",
            };

            var administratorRole = new AppRole
            {
                AppRoleId = (int)Role.Admin,
                Name = "Admin",
            };

            await context.AppRoles.AddRangeAsync(new[] { administratorRole, userRole });

            var administrator = new AppUser
            {
                Id = Constant.AdminId,
                FirstName = "Admin",
                LastName = "Admin",
                Role = Role.Admin,
                Status = Status.Active,
                Email = "Admin@gmail.com",
                UserName = "Admin"
            };

            var User = new AppUser
            {
                Id = Constant.UserId,
                FirstName = "testuser",
                LastName = "testuser",
                Role = Role.user,
                Status = Status.Active,
                Email = "testuser@gmail.com",
                UserName = "testuser",
                CreatedById = Constant.AdminId,
                Created = DateTime.Now
            };

            var User1 = new AppUser
            {
                Id = Constant.UserId1,
                FirstName = "testuser1",
                LastName = "testuser1",
                Role = Role.user,
                Status = Status.Active,
                Email = "testuser1@gmail.com",
                UserName = "testuser1",
                CreatedById = Constant.AdminId,
                Created = DateTime.Now
            };

            await context.AppUsers.AddRangeAsync(new[] { User, User1,  administrator});

            //set Admin password
            administrator.PasswordHash = userManager.PasswordHasher.HashPassword(administrator, "Password1");
            await userManager.UpdateAsync(administrator);

            //set User testuser
            User.PasswordHash = userManager.PasswordHasher.HashPassword(User, "Testpassword@01");
            await userManager.UpdateAsync(User);

            //set User testuser1
            User.PasswordHash = userManager.PasswordHasher.HashPassword(User1, "Testpassword@01");
            await userManager.UpdateAsync(User);

            await context.SaveChangesAsync();
        }
    }
}