using MediatR;

namespace Viseo.Application.Request.AuthRequest.GetAuth
{
    public class GetAuthQuery : IRequest<UserTokenVm>
    {
        public string username { get; set; }
        public string Password { get; set; }
    }
}