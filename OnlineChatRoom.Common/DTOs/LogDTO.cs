using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineChatRoom.Common.DTOs
{
    public class LogDTO
    {
        public string Message { get; set; }

        public string StackTrace { get; set; }

        public DateTime Date { get; set; }

        public int Id { get; set; }
    }
}
