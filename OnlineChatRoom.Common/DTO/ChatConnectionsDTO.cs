namespace OnlineChatRoom.Common.DTO
{
    using System;

    public class ChatConnectionsDTO
    {
        public Guid Id
        {
            get;
            set;
        }

        public String UserId
        {
            get;
            set;
        }

        public String RoomName
        {
            get;
            set;
        }

        public String UserAgent
        {
            get;
            set;
        }

        public RoomsDTO RoomNameNavigation
        {
            get;
            set;
        }

        public AspNetUsersDTO User
        {
            get;
            set;
        }
    }
}