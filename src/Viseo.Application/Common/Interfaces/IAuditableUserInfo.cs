using Viseo.Domain.Entities;

namespace Viseo.Application.Common.Interfaces
{
    public interface IAuditableUserInfo
    {
        AppUser CreatedBy { get; set; }
    }
}