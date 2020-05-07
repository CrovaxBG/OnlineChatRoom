using System;
using System.Collections.Generic;

namespace OnlineChatRoom.DataAccess.Models
{
    public partial class Rooms
    {
        public Rooms()
        {
            Connections = new HashSet<Connections>();
        }

        public string RoomName { get; set; }
        public bool IsPrivate { get; set; }

        public virtual ICollection<Connections> Connections { get; set; }
    }
}
