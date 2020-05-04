using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace OnlineChatRoom.DataAccess.Models
{
    public partial class AspNetUserRoles : IdentityUserRole<string>
    {
        public virtual AspNetRoles Role { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
