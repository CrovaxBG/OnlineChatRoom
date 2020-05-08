using OnlineChatRoom.Common.DTOs;

namespace OnlineChatRoom.Common.DTO
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;

    public class ConnectionsDTO
    {
        public Int32 Id
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