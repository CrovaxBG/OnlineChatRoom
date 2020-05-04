using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace OnlineChatRoom.DataAccess.Models
{
    public partial class AspNetUserTokens : IdentityUserToken<string>
    {
        public virtual AspNetUsers User { get; set; }
    }
}
