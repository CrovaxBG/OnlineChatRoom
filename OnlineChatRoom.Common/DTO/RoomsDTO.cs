using System.Collections.Generic;

namespace OnlineChatRoom.Common.DTO
{
    public class RoomsDTO
    {
        public string RoomName { get; set; }
        public bool IsPrivate { get; set; }

        public List<ChatConnectionsDTO> Connections { get; set; }
    }
}
