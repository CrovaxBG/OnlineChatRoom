using System;

namespace OnlineChatRoom.Common.DTO
{
    public class LogDTO
    {
        public string Message { get; set; }

        public string StackTrace { get; set; }

        public DateTime Date { get; set; }

        public int Id { get; set; }
    }
}
