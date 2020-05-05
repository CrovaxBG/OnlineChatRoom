using System;
using OnlineChatRoom.Common;

namespace OnlineChatRoom.ViewModels
{
    public class SystemMessageViewModel
    {
        public DateTime Timestamp { get; set; }
        public string FormattedTimestamp => Timestamp.FormatToTimestamp();

        public string Message { get; set; }
    }
}