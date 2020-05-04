using System;
using System.Collections.Generic;

namespace OnlineChatRoom.DataAccess.Models
{
    public partial class AspNetUsersSession
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime? LogoutDate { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
