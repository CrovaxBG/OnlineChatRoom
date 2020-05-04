using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace OnlineChatRoom.DataAccess.Models
{
    public partial class AspNetRoles : IdentityRole
    {
        public virtual ICollection<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
    }
}
