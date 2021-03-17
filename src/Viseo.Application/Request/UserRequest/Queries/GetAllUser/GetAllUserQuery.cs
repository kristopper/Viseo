using System.Collections.Generic;
using System.Linq;
using MediatR;

namespace Viseo.Application.Request.UserRequest.Queries.GetAllUser
{
    public class GetAllUserQuery : IRequest<List<UserDto>>
    {
        
    }
}