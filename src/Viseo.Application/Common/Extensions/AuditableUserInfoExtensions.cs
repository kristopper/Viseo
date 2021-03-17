using Viseo.Domain.Entities;

namespace Viseo.Application.Common.Extensions
{
    public static class AuditableUserInfoExtensions
    {
        public static string GetCreatorFullName(this AppUser userInfo)
        {
            if (userInfo.CreatedBy == null)
            {
                return string.Empty;
            }

            return ($"{userInfo.CreatedBy.FirstName} {userInfo.CreatedBy.LastName}");
        }

        public static string GetModifierFullName(this AppUser userInfo)
        {
            if (userInfo.LastModifiedBy == null)
            {
                return string.Empty;
            }

            return ($"{userInfo.LastModifiedBy.FirstName} {userInfo.LastModifiedBy.LastName}");
        }
    }
}