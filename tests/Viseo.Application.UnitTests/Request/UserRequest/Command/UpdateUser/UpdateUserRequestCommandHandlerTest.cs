using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using Viseo.Application.Common.Exceptions;
using Viseo.Application.Request.UserRequest.Command.UpdateUser;
using Viseo.Application.UnitTests.Common;
using Viseo.Domain.Common.Enums;
using Viseo.Domain.Entities;
using Xunit;

namespace Viseo.Application.UnitTests.Request.UserRequest.Command.UpdateUser
{
    public class UpdateUserRequestCommandHandlerTest : CommandTestBase
    {
        private readonly UpdateUserRequestCommandHandler _sut;
        public UpdateUserRequestCommandHandlerTest()
        {
            _sut = new UpdateUserRequestCommandHandler(_mockUserManager.Object, _mockCurrentUser.Object, _context);
        }

        [Fact]
        public async Task Handle_UserTypeUserUpdateOtherUserInfo_ShouldThrowCannotUpdateException()
        {
            _mockCurrentUser.Setup(s => s.Id).Returns("c4019954-cb56-4981-8aee-111111111111");
            _mockCurrentUser.Setup(s => s.Role).Returns(Role.user);

            var query = new UpdateUserRequestCommand
            {
                Id = "c4019954-cb56-4981-8aee-222222222222",
                FirstName = "testuserupdate",
                LastName = "testuser1",
                Email = "testuser1@gmail.com",
                Username = "testuser",
                Role = Role.user,
                Status = Status.Active
            };

            await Assert.ThrowsAsync<CannotUpdateException>(() => _sut.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_GivenExistingEmail_ShouldThrowEntityValueExistsException()
        {
            _mockCurrentUser.Setup(s => s.Id).Returns("c4019954-cb56-4981-8aee-222222222222");
            _mockCurrentUser.Setup(s => s.Role).Returns(Role.user);

            var query = new UpdateUserRequestCommand
            {
                Id = "c4019954-cb56-4981-8aee-222222222222",
                FirstName = "testuser",
                LastName = "testuser",
                Email = "testuser1@gmail.com",
                Username = "testuser",
                Role = Role.user,
                Status = Status.Active
            };

            await Assert.ThrowsAsync<EnityValueExistsException>(() => _sut.Handle(query, CancellationToken.None));
        }
        
        [Fact]
        public async Task Handle_GivenExistingUsername_ShouldThrowEnityValueExistsException()
        {
            _mockCurrentUser.Setup(s => s.Id).Returns("c4019954-cb56-4981-8aee-111111111111");
            _mockCurrentUser.Setup(s => s.Role).Returns(Role.user);

            var query = new UpdateUserRequestCommand
            {
                Id = "c4019954-cb56-4981-8aee-111111111111",
                FirstName = "testuser",
                LastName = "testuser",
                Email = "testuser@gmail.com",
                Username = "testuser1",
                Role = Role.user,
                Status = Status.Active
            };

            await Assert.ThrowsAsync<EnityValueExistsException>(() => _sut.Handle(query, CancellationToken.None));
        }
        
        [Fact]
        public async Task Handle_GivenUserInfo_ShouldReturnIdentityResult()
        {
            _mockCurrentUser.Setup(s => s.Id).Returns("c4019954-cb56-4981-8aee-111111111111");
            _mockCurrentUser.Setup(s => s.Role).Returns(Role.user);

            var query = new UpdateUserRequestCommand
            {
                Id = "c4019954-cb56-4981-8aee-111111111111",
                FirstName = "testuserupdate",
                LastName = "testuser",
                Email = "testuser@gmail.com",
                Username = "testuserupdate",
                Role = Role.user,
                Status = Status.Active
            };

            _mockUserManager.Setup(s => s.UpdateAsync(It.IsAny<AppUser>())).ReturnsAsync(IdentityResult.Success);
            var result = await _sut.Handle(query, CancellationToken.None);
    
            Assert.NotNull(result);
            Assert.IsType<IdentityResult>(result);
            Assert.Equal(IdentityResult.Success, result);
        }
    }
}