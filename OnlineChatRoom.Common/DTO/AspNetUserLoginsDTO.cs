namespace OnlineChatRoom.Common.DTO
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;

    public class AspNetUserLoginsDTO
    {
        public AspNetUsersDTO User
        {
            get;
            set;
        }

        public String LoginProvider
        {
            get;
            set;
        }

        public String ProviderKey
        {
            get;
            set;
        }

        public String ProviderDisplayName
        {
            get;
            set;
        }

        public String UserId
        {
            get;
            set;
        }
    }
}