using Viseo.Domain.Entities;

namespace Viseo.Domain.Common.Interfaces
{
    public interface IAuditableUserInfo
    {
        AppUser CreatedBy { get; set; }
        AppUser ModifiedBy { get; set; }
    }
}