namespace OnlineChatRoom.Common.DTO
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;

    public class AspNetRolesDTO
    {
        public ICollection<AspNetRoleClaimsDTO> AspNetRoleClaims
        {
            get;
            set;
        }

        public ICollection<AspNetUserRolesDTO> AspNetUserRoles
        {
            get;
            set;
        }

        public String Id
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public String NormalizedName
        {
            get;
            set;
        }

        public String ConcurrencyStamp
        {
            get;
            set;
        }
    }
}