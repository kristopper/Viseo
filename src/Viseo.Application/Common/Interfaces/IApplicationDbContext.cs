using Viseo.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Viseo.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<AppUser> AppUsers { get; set; }
        DbSet<AppRole> AppRoles { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}