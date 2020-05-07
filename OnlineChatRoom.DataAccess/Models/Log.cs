using System;
using System.Collections.Generic;

namespace OnlineChatRoom.DataAccess.Models
{
    public partial class Log
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime Date { get; set; }
    }
}
