using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Viseo.Application.AuthRequest.Queries.RefreshTokenAuth;
using Viseo.Application.Common.Interfaces;
using Viseo.Application.Common.Models;
using Viseo.Application.Request.AuthRequest;
using Viseo.Application.Request.AuthRequest.GetAuth;
using Viseo.Application.UnitTests.Common;
using Viseo.Domain.Common.Enums;
using Xunit;

namespace Viseo.Application.UnitTests.Request.AuthRequest.Queries.RefreshTokenAuth
{
    public class RefreshTokenAuthQueryHandlerTest
    {
        private readonly RefreshTokenAuthQueryHandler _sut;
        private readonly Mock<ICurrentUserService> _mockCurrentUser = new Mock<ICurrentUserService>();
        private readonly Mock<IJwtAuthManager> _mockJwtAuthManager = new Mock<IJwtAuthManager>();
        private readonly QueryTestFixture _queryTestFixture = new QueryTestFixture();
        public RefreshTokenAuthQueryHandlerTest()
        {
            _sut = new RefreshTokenAuthQueryHandler(_mockJwtAuthManager.Object, _mockCurrentUser.Object);
        }

        [Fact]
        public async Task Handle_GivenCredential_ShouldReturnUserTokenVm()
        {
            _mockCurrentUser.Setup(s => s.Id).Returns(Constant.AdminId);
            _mockCurrentUser.Setup(s => s.Name).Returns("Admin");
            _mockCurrentUser.Setup(s => s.Role).Returns(Role.Admin);

            var query = new RefreshTokenAuthQuery
            {
                refreshToken = "refreshtoken"
            };

            _mockJwtAuthManager.Setup(x => x.Refresh(It.IsAny<string>(), It.IsAny<Claim[]>(), It.IsAny<DateTime>())).Returns(new JwtAuthResult()
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
    }
}