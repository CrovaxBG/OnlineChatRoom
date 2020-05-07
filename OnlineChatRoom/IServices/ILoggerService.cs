using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineChatRoom.Common.DTOs;

namespace OnlineChatRoom.IServices
{
    public interface ILoggerService
    {
        Task<int> LogAsync(LogDTO logData);
        Task<int> LogAsync(string message);
        Task<LogDTO> GetLogAsync(int id);
        Task<List<LogDTO>> GetLogsAsync();
    }
}
