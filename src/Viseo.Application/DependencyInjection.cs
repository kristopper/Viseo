using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Viseo.Application.Common.Interfaces;
using Viseo.Application.Services;

namespace Viseo.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(assemblies: Assembly.GetExecutingAssembly());
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}