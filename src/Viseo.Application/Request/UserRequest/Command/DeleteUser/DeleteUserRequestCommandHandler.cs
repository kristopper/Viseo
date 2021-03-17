using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Viseo.Application.Common.Exceptions;
using Viseo.Application.Common.Interfaces;
using Viseo.Domain.Common.Enums;
using Viseo.Domain.Entities;

namespace Viseo.Application.Request.UserRequest.Command.DeleteUser
{
    public class DeleteUserRequestCommandHandler : IRequestHandler<DeleteUserRequestCommand, IdentityResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IApplicationDbContext _context;

        public DeleteUserRequestCommandHandler(UserManager<AppUser> userManager, IApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IdentityResult> Handle(DeleteUserRequestCommand request, CancellationToken cancellationToken)
        {
            
            var appUser = await _context.AppUsers
                .Where(o => o.Id == request.Id)
                .SingleOrDefaultAsync();
           
            _ = appUser ?? throw new NotFoundException("Id", request.Id);

            appUser.Status = Status.Deleted;

            IdentityResult result = await _userManager.UpdateAsync(appUser);
            
            return (result);
        }
    }
}