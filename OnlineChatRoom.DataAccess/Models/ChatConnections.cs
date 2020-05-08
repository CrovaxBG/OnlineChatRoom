using System;
using System.Collections.Generic;

namespace OnlineChatRoom.DataAccess.Models
{
    public partial class ChatConnections
    {
        public Guid ConnectionId { get; set; }
        public string UserId { get; set; }
        public string RoomName { get; set; }
        public string UserAgent { get; set; }

        public virtual Rooms RoomNameNavigation { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
