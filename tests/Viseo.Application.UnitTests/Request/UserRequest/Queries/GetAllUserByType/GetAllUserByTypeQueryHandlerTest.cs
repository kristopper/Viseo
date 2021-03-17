using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Viseo.Application.Common.Exceptions;
using Viseo.Application.Common.Interfaces;
using Viseo.Application.UnitTests.Common;
using Viseo.Domain.Common.Enums;
using Xunit;

namespace Viseo.Application.Request.UserRequest.Queries.GetAllUserByType
{
    public class GetAllUserByTypeQueryHandlerTest
    {
        private readonly Mock<ICurrentUserService> _mockCurrentUser;
        private readonly GetAllUserByTypeQueryHandler _sut;
        private readonly QueryTestFixture _queryTestFixture = new QueryTestFixture();
        public GetAllUserByTypeQueryHandlerTest()
        {
            _mockCurrentUser = _queryTestFixture.MockCurrentUser;

            _sut = new GetAllUserByTypeQueryHandler(_queryTestFixture.Context, _queryTestFixture.Mapper, _mockCurrentUser.Object);
        }

        [Fact]
        public async Task Handle_GivenNotExistingUserType_ShouldThrowNOutOfRangeException()
        {
            _mockCurrentUser.Setup(s => s.Role).Returns(Role.Admin);
            var query = new GetAllUserByTypeQuery
            {
                UserType = (Displayrole)100_000,
                Search = "",
                pageNumber = 0,
                pageSize = 10,
            };

            await Assert.ThrowsAsync<OutOfRangeException>(() => _sut.Handle(query, CancellationToken.None));
        }

        [Theory]
        [InlineData(Displayrole.user)]
        [InlineData(Displayrole.Admin)]
        public async Task Handle_GivenValidUserTypes_ShouldReturnExpectedUserType(Displayrole displayRole)
        {
            _mockCurrentUser.Setup(s => s.Role).Returns(Role.Admin);

            var query = new GetAllUserByTypeQuery
            {
                UserType = displayRole,
                Search = "",
                pageNumber = 0,
                pageSize = 10,
            };

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.IsType<UserVm>(result);
            Assert.All<UserDto>(result.UserList, (data) =>
            {
                Assert.Equal(displayRole.ToString(), data.Role);
            });
        }

        [Fact]
        public async Task Handle_GivenUserTypesAll_ShouldReturnExpectedAllUserType()
        {
            _mockCurrentUser.Setup(s => s.Role).Returns(Role.Admin);

            var query = new GetAllUserByTypeQuery
            {
                UserType = Displayrole.All,
                Search = "",
                pageNumber = 0,
                pageSize = 10,
            };

            var result = await _sut.Handle(query, CancellationToken.None);
            Assert.IsType<UserVm>(result);
        }

        [Fact]
        public async Task Handle_GivenSearch_ShouldReturnExpectedSearchkey()
        {
            _mockCurrentUser.Setup(s => s.Role).Returns(Role.Admin);

            var query = new GetAllUserByTypeQuery
            {
                UserType = Displayrole.All,
                Search = "testuser",
                pageNumber = 0,
                pageSize = 10,
            };

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.All<UserDto>(result.UserList, (data) =>
            {
                Assert.Contains(query.Search, data.FirstName);
                Assert.Contains(query.Search, data.LastName);
                Assert.Contains(query.Search, data.Email);
            });
        }

        [Fact]
        public async Task Handle_GivenPagination_ShouldNotEqualToDifferentPages()
        {
            _mockCurrentUser.Setup(s => s.Role).Returns(Role.user);

            var query1 = new GetAllUserByTypeQuery
            {
                UserType = Displayrole.user,
                Search = "",
                pageNumber = 0,
                pageSize = 1,
            };

            var query2 = new GetAllUserByTypeQuery
            {
                UserType = Displayrole.user,
                Search = "",
                pageNumber = 1,
                pageSize = 1,
            };

            var result1 = await _sut.Handle(query1, CancellationToken.None);
            var result2 = await _sut.Handle(query2, CancellationToken.None);

            Assert.Equal(result1.Total, result2.Total);
            Assert.NotEqual(result1.UserList.First().Id, result2.UserList.First().Id);
        }
    }
}