using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using Viseo.Application.Common.Configuration;
using Viseo.Application.Common.Exceptions;
using Viseo.Application.Common.Interfaces;
using Viseo.Application.Common.Models;
using Viseo.Application.Request.AuthRequest;
using Viseo.Application.Request.AuthRequest.GetAuth;
using Viseo.Application.UnitTests.Common;
using Viseo.Domain.Common.Enums;
using Viseo.Domain.Entities;
using Xunit;

namespace Viseo.Application.UnitTests.Request.AuthRequest.Queries.GetAuth
{
    public class GetAuthQueryHandlerTest
    {
        private readonly GetAuthQueryHandler _sut;
        public Mock<UserManager<AppUser>> mockUserManager;
        private readonly Mock<IJwtAuthManager> _mockJwtAuthManager = new Mock<IJwtAuthManager>();
        public QueryTestFixture _queryTestFixture = new QueryTestFixture();
        public GetAuthQueryHandlerTest()
        {
            mockUserManager = _queryTestFixture._mockUserManager;

            _sut = new GetAuthQueryHandler(
                mockUserManager.Object,
                _queryTestFixture.Mapper,
                _mockJwtAuthManager.Object,
                 _queryTestFixture.Context);
        }

        [Fact]
        public async Task Handle_GivenIncorrectUsername_ShouldThrowNotFoundException()
        {
            var query = new GetAuthQuery
            {
                username = "Admin1",
                Password = "Password1",
            };

            await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_GivenIncorrectPassword_ShouldThrowUserPasswordNotCorrectException()
        {
            var query = new GetAuthQuery
            {
                username = "Admin",
                Password = "Password@1",
            };

            mockUserManager.Setup(s => s.CheckPasswordAsync(It.IsAny<AppUser>(), It.IsAny<string>())).ReturnsAsync(false);

            await Assert.ThrowsAsync<UserPasswordNotCorrectException>(() => _sut.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_GivenCredential_ShouldReturnUserTokenVm()
        {
            var query = new GetAuthQuery
            {
                username = "Admin",
                Password = "Password1",
            };

            mockUserManager.Setup(s => s.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(GetAppUser());

            mockUserManager.Setup(s => s.CheckPasswordAsync(It.IsAny<AppUser>(), It.IsAny<string>())).ReturnsAsync(true);

            _mockJwtAuthManager.Setup(x => x.GenerateTokens(It.IsAny<string>(), It.IsAny<Claim[]>(), It.IsAny<DateTime>())).Returns(new JwtAuthResult()
            {
                AccessToken = "token",
                RefreshToken = GetRefreshToken(),
            });

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.IsType<UserTokenVm>(result);
        }

        private RefreshToken GetRefreshToken() => new RefreshToken
        {
            UserName = "Admin",
            TokenString = "token",
            ExpireAt = DateTime.Now
        };

        private AppUser GetAppUser() => new AppUser
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

    }
}