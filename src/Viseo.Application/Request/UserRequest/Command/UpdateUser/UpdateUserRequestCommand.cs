using MediatR;
using Microsoft.AspNetCore.Identity;
using Viseo.Domain.Common.Enums;

namespace Viseo.Application.Request.UserRequest.Command.UpdateUser
{
    public class UpdateUserRequestCommand : IRequest<IdentityResult>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public Status Status { get; set; }
    }
}