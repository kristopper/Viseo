using MediatR;
using Viseo.Application.Request.AuthRequest;

namespace Viseo.Application.AuthRequest.Queries.RefreshTokenAuth
{
    public class RefreshTokenAuthQuery : IRequest<UserTokenVm>
    {
        public string refreshToken { get; set; }
    }

}