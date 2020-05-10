using System;
using System.Collections.Generic;

namespace OnlineChatRoom.DataAccess.Models
{
    public partial class Rooms
    {
        public Rooms()
        {
            ChatConnections = new List<ChatConnections>();
        }

        public string RoomName { get; set; }
        public bool IsPrivate { get; set; }

        //
        public virtual ICollection<ChatConnections> ChatConnections { get; set; }
    }
}
