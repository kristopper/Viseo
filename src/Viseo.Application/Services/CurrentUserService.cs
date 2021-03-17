using System;
using System.Security.Claims;
using Viseo.Application.Common.Interfaces;
using Viseo.Domain.Common.Enums;
using Microsoft.AspNetCore.Http;

namespace Viseo.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            Id = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (Id != null)
            {
                Name = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
                IsAuthenticated = true;
                string roleName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
                if (Enum.TryParse(roleName, out Role role))
                {
                    Role = role;
                }
                else
                {
                    throw new InvalidCastException($"Cannot cast role '{roleName}'");
                }
            }
        }

        public string Id { get; }
        public string Name { get; }
        public bool IsAuthenticated { get; }
        public Role Role { get; }
    }
}