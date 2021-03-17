using System;
using Viseo.Domain.Common.Enums;
using Microsoft.AspNetCore.Identity;
using Viseo.Domain.Common;
using Viseo.Domain.Common.Interfaces;
using System.Collections.Generic;

namespace Viseo.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        public Status Status { get; set; }
        public string CreatedById { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedById { get; set; }
        public DateTime? LastModified { get; set; }

        public virtual AppUser CreatedBy { get; set;}
        public virtual AppUser LastModifiedBy { get; set; }

        public virtual ICollection<AppUser> CreatedUsers { get; set; }
        public virtual ICollection<AppUser> ModifiedUsers { get; set; }
    }
}