using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace OnlineChatRoom.DataAccess.Models
{
    public partial class AspNetRoleClaims : IdentityRoleClaim<string>
    {
        public virtual AspNetRoles Role { get; set; }
    }
}
