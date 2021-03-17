using MediatR;
using Viseo.Domain.Common.Enums;

namespace Viseo.Application.Request.UserRequest.Queries.GetAllUserByType
{
    public class GetAllUserByTypeQuery : IRequest<UserVm>
    {
        public Displayrole UserType { get; set; }
        public string Search { get; set; }
        public int pageNumber { get; set; } = 0;
        public int pageSize { get; set; } = 10;
    }
}