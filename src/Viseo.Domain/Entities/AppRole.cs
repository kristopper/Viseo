using Microsoft.AspNetCore.Identity;

namespace Viseo.Domain.Entities
{
    public class AppRole :  IdentityRole
    {
        public int AppRoleId { get; set; }
        public AppRole() : base()
        {
        }

        public AppRole(string roleName) : base(roleName)
        {
        }
    }
}