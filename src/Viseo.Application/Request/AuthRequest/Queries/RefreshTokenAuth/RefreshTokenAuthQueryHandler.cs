using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Viseo.Application.Common.Interfaces;
using Viseo.Application.Request.AuthRequest;
using System.Security.Claims;

namespace Viseo.Application.AuthRequest.Queries.RefreshTokenAuth
{
    public class RefreshTokenAuthQueryHandler : IRequestHandler<RefreshTokenAuthQuery, UserTokenVm>
    {
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly ICurrentUserService _currentUserService;
        public RefreshTokenAuthQueryHandler(IJwtAuthManager jwtAuthManager, ICurrentUserService currentUserService)
        {
            _jwtAuthManager = jwtAuthManager;
           _currentUserService = currentUserService;
        }

        public async Task<UserTokenVm> Handle(RefreshTokenAuthQuery request, CancellationToken cancellationToken)
        {
            DateTime now = DateTime.Now;
            await Task.Delay(1); 
            var user = _currentUserService.Name;
            var id = _currentUserService.Id;
            var role = _currentUserService.Role.ToString();

            var jwtResult = _jwtAuthManager.Refresh(request.refreshToken, CreateUserClaim(user, id, role), DateTime.Now);

            return new UserTokenVm
            {
                UserName = user,
                Role = role,
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString
            };
        }
        private Claim[] CreateUserClaim(string user, string id, string role) =>
            new[]
            {
                new Claim(ClaimTypes.Name, user),
                new Claim(ClaimTypes.Role, id),
                new Claim(ClaimTypes.NameIdentifier, role),
            };
    }

}