using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Viseo.Application.Request.UserRequest.Command.CreateUser;
using Moq;
using Viseo.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using Microsoft.Extensions.Logging;
using Xunit;
using Viseo.Domain.Common.Enums;
using System.Linq;
using Viseo.Application.Common.Exceptions;
using Viseo.Application.UnitTests.Common;
using Viseo.Application.Common.Interfaces;
using Viseo.Application.Common.Extensions;

namespace Viseo.Application.UnitTests.Request.UserRequest.CreateUser
{
    public class CreateUserRequestCommandHandlerTest : CommandTestBase
    {
        private readonly CreateUserRequestCommandHandler _sut;

        public CreateUserRequestCommandHandlerTest()
        {

            _sut = new CreateUserRequestCommandHandler(_mockUserManager.Object, _context);

        }

        [Fact]
        public async Task Handle_GivenExistingEmail_ShouldThrowEntityValueExistsException()
        {
            var query = new CreateUserRequestCommand
            {
                FirstName = "testuser2",
                LastName = "testuser2",
                Email = "testuser@gmail.com",
                Username = "testuser2",
                Password = "Testpassword",
                Role = Role.user
            };

            //GetAdminUser().GetCreatorFullName()
            await Assert.ThrowsAsync<EnityValueExistsException>(() => _sut.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_GivenExistingUsername_ShouldThrowEnityValueExistsException()
        {

            var query = new CreateUserRequestCommand
            {
                FirstName = "testuser2",
                LastName = "testuser2",
                Email = "testuser2@gmail.com",
                Username = "testuser",
                Password = "Testpassword",
                Role = Role.user
            };

            await Assert.ThrowsAsync<EnityValueExistsException>(() => _sut.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_GivenUserInfo_ShouldReturnIdentityResult()
        {
            var query = new CreateUserRequestCommand
            {
                FirstName = "testuser2",
                LastName = "testuser2",
                Email = "testuser2@gmail.com",
                Username = "testuser2",
                Password = "Password@01",
                Role = Role.user
            };

            _mockUserManager.Setup(s => s.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            var result = await _sut.Handle(query, CancellationToken.None);
    
            Assert.NotNull(result);
            Assert.IsType<IdentityResult>(result);
            Assert.Equal(IdentityResult.Success, result);
        }

        /*
        private AppUser GetAdminUser() => new AppUser
            {
                Id = Constant.UserId,
                FirstName = "Admin",
                LastName = "Admin",
                Role = Role.Admin,
                Status = Status.Active,
                Email = "Admin@gmail.com",
                UserName = "Admin"
            };
            */

    }
}