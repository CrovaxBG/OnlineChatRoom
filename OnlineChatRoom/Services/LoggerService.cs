using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using OnlineChatRoom.Common;
using OnlineChatRoom.Common.DTOs;
using OnlineChatRoom.Infrastructure.Controllers;
using OnlineChatRoom.Infrastructure.Implementations.Controllers;
using OnlineChatRoom.IServices;

namespace OnlineChatRoom.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly HttpClient _client;

        public LoggerService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public Task<int> LogAsync(string message)
        {
            return LogAsync(new LogDTO { Message = message, Date = DateTime.Now});
        }

        public async Task<int> LogAsync(LogDTO logData)
        {
            var response = await _client.PostAsJsonAsync(nameof(LoggerController.AddLog), logData);

            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                if (int.TryParse(message, out var id))
                {
                    return id;
                }
            }

            return -1;
        }

        public async Task<LogDTO> GetLogAsync(int id)
        {
            var response = await _client.GetAsync($"{nameof(LoggerController.GetLog)}?logId={id}");

            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsJsonAsync<LogDTO>();
                return message;
            }

            return null;
        }

        public async Task<List<LogDTO>> GetLogsAsync()
        {
            var response = await _client.GetAsync(nameof(LoggerController.GetLogs));

            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsJsonAsync<List<LogDTO>>();
                return message;
            }

            return new List<LogDTO>();
        }
    }
}
