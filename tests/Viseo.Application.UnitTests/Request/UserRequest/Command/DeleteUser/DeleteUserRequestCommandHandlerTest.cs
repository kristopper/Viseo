using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using Viseo.Application.Common.Exceptions;
using Viseo.Application.Request.UserRequest.Command.DeleteUser;
using Viseo.Application.UnitTests.Common;
using Viseo.Domain.Common.Enums;
using Viseo.Domain.Entities;
using Xunit;

namespace Viseo.Application.UnitTests.Request.UserRequest.Command.DeleteUser
{
    public class DeleteUserRequestCommandHandlerTest : CommandTestBase
    {
        private readonly DeleteUserRequestCommandHandler _sut;

        public DeleteUserRequestCommandHandlerTest()
        {
            _sut = new DeleteUserRequestCommandHandler(_mockUserManager.Object, _context);
        }

        [Fact]
        public async Task Handle_GivenNotExistingId_ShouldThrowNotFoundException()
        {
            var query = new DeleteUserRequestCommand
            {
                Id = "123123132"
            };

            await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_GivenId_ShouldReturnIdentityResult()
        {
            var query = new DeleteUserRequestCommand
            {
                Id = "c4019954-cb56-4981-8aee-222222222222"
            };

            _mockUserManager.Setup(s => s.UpdateAsync(It.IsAny<AppUser>())).ReturnsAsync(IdentityResult.Success);
            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<IdentityResult>(result);
            Assert.Equal(IdentityResult.Success, result);
        }
    }
}