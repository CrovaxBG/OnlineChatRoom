using System;
using System.Net.Http;
using System.Threading.Tasks;
using OnlineChatRoom.Common;
using OnlineChatRoom.Common.DTO;
using OnlineChatRoom.Infrastructure.Controllers;
using OnlineChatRoom.IServices;

namespace OnlineChatRoom.Services
{
    public class ChatConnectionsService : IChatConnectionsService
    {
        private readonly HttpClient _client;

        public ChatConnectionsService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<ChatConnectionsDTO> GetChatConnectionAsync(Guid connectionId)
        {
            var response = await _client.GetAsync($"{nameof(ChatConnectionsController.GetChatConnection)}?connectionId={connectionId}");

            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsJsonAsync<ChatConnectionsDTO>();
                return message;
            }

            return null;
        }

        public async Task DeleteChatConnectionAsync(Guid connectionId)
        {
            await _client.DeleteAsJsonAsync(nameof(ChatConnectionsController.DeleteChatConnection), connectionId);
        }

        public async Task<string> CreateChatConnectionAsync(ChatConnectionsDTO connectionData)
        {
            var response = await _client.PostAsJsonAsync(nameof(ChatConnectionsController.CreateChatConnection), connectionData);

            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                return message;
            }

            return null;
        }
    }
}