using System;
using System.Collections.Generic;
using System.Text;
using OnlineChatRoom.Common.DTO;

namespace OnlineChatRoom.Common.DTOs
{
    public class RoomsDTO
    {
        public string RoomName { get; set; }
        public bool IsPrivate { get; set; }

        public List<ConnectionsDTO> Connections { get; set; }
    }
}
