namespace OnlineChatRoom.Common.DTO
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;

    public class AspNetUserClaimsDTO
    {
        public AspNetUsersDTO User
        {
            get;
            set;
        }

        public Int32 Id
        {
            get;
            set;
        }

        public String UserId
        {
            get;
            set;
        }

        public String ClaimType
        {
            get;
            set;
        }

        public String ClaimValue
        {
            get;
            set;
        }
    }
}