using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace OnlineChatRoom.DataAccess.Models
{
    public partial class ChatConnections
    {
        public string ConnectionId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("RoomName")]
        public string RoomName { get; set; }
        public string UserAgent { get; set; }

        public virtual Rooms RoomNameNavigation { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
