using Viseo.Domain.Common.Enums;

namespace Viseo.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string Id { get; }
        string Name { get; }
        bool IsAuthenticated { get; }
        Role Role { get; }
    }
}
