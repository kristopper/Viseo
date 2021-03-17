using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Viseo.Application.UnitTests.Common;
using Xunit;

namespace Viseo.Application.Request.UserRequest.Queries.GetAllUser
{
    public class GetAllUserQueryHandlerTest
    {
        private readonly GetAllUserQueryHandler _sut;
        private readonly QueryTestFixture _queryTestFixture = new QueryTestFixture();

        public GetAllUserQueryHandlerTest()
        {
            _sut = new GetAllUserQueryHandler(_queryTestFixture.Context, _queryTestFixture.Mapper);
        }

        [Fact]
        public async Task Handle__ShouldReturnAllUser()
        {
            var query = new GetAllUserQuery
            {
            };

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.IsType<List<UserDto>>(result);
        }
    }
}