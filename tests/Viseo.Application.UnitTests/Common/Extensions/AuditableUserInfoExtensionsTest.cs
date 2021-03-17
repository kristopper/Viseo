using System;
using Moq;
using Viseo.Application.Common.Extensions;
using Viseo.Domain.Common.Enums;
using Viseo.Domain.Entities;
using Xunit;

namespace Viseo.Application.UnitTests.Common.Extensions
{
    public class AuditableUserInfoExtensionsTest
    {

        //public Mock<AuditableUserInfoExtensions> _auditableuserinformation = new Mock<GetCreatorFullName>();
        public AuditableUserInfoExtensionsTest()
        {
        }


        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            var user = new AppUser()
            {
                Id = Constant.AdminId,
                FirstName = "Admin",
                LastName = "Admin",
                Role = Role.Admin,
                Status = Status.Active,
                Email = "Admin@gmail.com",
                UserName = "Admin",
                CreatedById = null,
                Created = DateTime.Now
            };

            var actual = AuditableUserInfoExtensions.GetCreatorFullName(It.IsAny<AppUser>());
            
            //var actual = AuditableUserInfoExtensions.GetCreatorFullName(user);
            //It.IsAny<AppUser>()
            //actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
        }

    }
}