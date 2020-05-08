using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineChatRoom.Common.DTOs;

namespace OnlineChatRoom.IServices
{
    public interface IRoomsService
    {
        Task<RoomsDTO> GetRoomAsync(string name);
        Task<IEnumerable<RoomsDTO>> GetRoomsAsync();
        Task RemoveRoomAsync(string name);
        Task<string> CreateRoomAsync(RoomsDTO roomData);
    }
}
