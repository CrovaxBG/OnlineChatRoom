using System;
using System.Threading.Tasks;
using OnlineChatRoom.Common.DTO;

namespace OnlineChatRoom.IServices
{
    public interface IChatConnectionsService
    {
        Task<ChatConnectionsDTO> GetChatConnectionAsync(string connectionId);
        Task DeleteChatConnectionAsync(string connectionId);
        Task<string> CreateChatConnectionAsync(ChatConnectionsDTO connectionData);
        Task UpdateChatConnectionAsync(ChatConnectionsDTO connectionData);
    }
}