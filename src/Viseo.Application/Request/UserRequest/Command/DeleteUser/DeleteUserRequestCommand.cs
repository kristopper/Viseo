using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Viseo.Application.Request.UserRequest.Command.DeleteUser
{
    public class DeleteUserRequestCommand : IRequest<IdentityResult>
    {
        public string Id { get; set; }
    }
}