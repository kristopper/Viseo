using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Viseo.Application.Common.Exceptions;
using Viseo.Application.Common.Interfaces;
using Viseo.Domain.Common.Enums;
using Viseo.Domain.Entities;

namespace Viseo.Application.Request.UserRequest.Command.CreateUser
{
    public class CreateUserRequestCommandHandler : IRequestHandler<CreateUserRequestCommand, IdentityResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IApplicationDbContext _context;
        public CreateUserRequestCommandHandler(UserManager<AppUser> userManager, IApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(CreateUserRequestCommand request, CancellationToken cancellationToken)
        {
            var useremailExist = _context.AppUsers.Where(e => e.Email == request.Email).Any();

            if (useremailExist)
            {
                throw new EnityValueExistsException("Email", request.Email);
            }

            var usernameExist = _context.AppUsers.Where(e => e.UserName == request.Username).Any();

            if (usernameExist)
            {
                throw new EnityValueExistsException("UserName", request.Username);
            }

            var info = new AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Username,
                Role = request.Role,
                Status = Status.Active,
            };
            
            IdentityResult result = await _userManager.CreateAsync(info, request.Password);

            return (result);
        }
    }
}