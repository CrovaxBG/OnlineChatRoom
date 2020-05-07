using OnlineChatRoom.DataAccess.Models;

namespace OnlineChatRoom.ViewModels
{
    public class ChatRoomViewModel
    {
        public string RoomName { get; set; }
        public AspNetUsers CurrentUser { get; set; }
    }
}