using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Viseo.Application.Common.Exceptions;
using Viseo.Application.Common.Interfaces;
using Viseo.Domain.Entities;

namespace Viseo.Application.Request.AuthRequest.GetAuth
{
    public class GetAuthQueryHandler : IRequestHandler<GetAuthQuery, UserTokenVm>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        public GetAuthQueryHandler(UserManager<AppUser> userManager, IMapper mapper, IJwtAuthManager jwtAuthManager, IApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _jwtAuthManager = jwtAuthManager;
        }

        public async Task<UserTokenVm> Handle(GetAuthQuery request, CancellationToken cancellationToken)
        {
            //var userinfo = await _userManager.FindByNameAsync(request.username);
            var userinfo =  _context.AppUsers.Where(n => n.UserName == request.username).FirstOrDefault();

            _ = userinfo ?? throw new NotFoundException("Username", request.username);

            var test = _userManager.PasswordHasher.HashPassword(userinfo, request.Password);
            
            if (!(await _userManager.CheckPasswordAsync(userinfo, request.Password)))
            {
                throw new UserPasswordNotCorrectException(request.username);
            }

            var jwtResult = _jwtAuthManager.GenerateTokens(userinfo.UserName, CreateUserClaim(userinfo), DateTime.Now);

            return new UserTokenVm
            {
                UserName = userinfo.UserName,
                Role = userinfo.Role.ToString(),
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString
            };
        }

        private Claim[] CreateUserClaim(AppUser user) =>
            new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };
    }
}