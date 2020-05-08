using System;
using System.Threading.Tasks;
using OnlineChatRoom.Common.DTO;

namespace OnlineChatRoom.IServices
{
    public interface IChatConnectionsService
    {
        Task<ChatConnectionsDTO> GetChatConnectionAsync(Guid connectionId);
        Task DeleteChatConnectionAsync(Guid connectionId);
        Task<string> CreateChatConnectionAsync(ChatConnectionsDTO connectionData);
    }
}