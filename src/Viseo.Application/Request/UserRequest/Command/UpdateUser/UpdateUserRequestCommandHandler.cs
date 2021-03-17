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

namespace Viseo.Application.Request.UserRequest.Command.UpdateUser
{
    public class UpdateUserRequestCommandHandler : IRequestHandler<UpdateUserRequestCommand, IdentityResult>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IApplicationDbContext _context;
        public UpdateUserRequestCommandHandler(UserManager<AppUser> userManager, ICurrentUserService currentUserService, IApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<IdentityResult> Handle(UpdateUserRequestCommand request, CancellationToken cancellationToken)
        {
            var id = _currentUserService.Id;
            var role = _currentUserService.Role;

            if (role == Role.user && id != request.Id)
            {
                throw new CannotUpdateException(role.ToString());
            }

            var useremailExist = _context.AppUsers.Where(e => e.Email == request.Email & e.Id != request.Id).Any();

            if (useremailExist)
            {
                throw new EnityValueExistsException("Email", request.Email);
            }

            var usernameExist = _context.AppUsers.Where(e => e.UserName == request.Username & e.Id != request.Id).Any();

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
                Status = request.Status,
            };

            IdentityResult result = await _userManager.UpdateAsync(info);

            return (result);
        }
    }
}