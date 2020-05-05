using System;
using OnlineChatRoom.Common;
using OnlineChatRoom.DataAccess.Models;

namespace OnlineChatRoom.ViewModels
{
    public class UserMessageViewModel
    {
        public DateTime Timestamp { get; set; }
        public string FormattedTimestamp => Timestamp.FormatToTimestamp();

        public AspNetUsers User { get; set; }
        public string Message { get; set; }
    }
}
