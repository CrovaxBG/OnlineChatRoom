using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using OnlineChatRoom.Common;
using OnlineChatRoom.Common.DTOs;
using OnlineChatRoom.Infrastructure.Controllers;
using OnlineChatRoom.IServices;

namespace OnlineChatRoom.Services
{
    public class RoomsService : IRoomsService
    {
        private readonly HttpClient _client;

        public RoomsService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<RoomsDTO> GetRoomAsync(string name)
        {
            var response = await _client.GetAsync($"{nameof(RoomsController.GetRooms)}?roomName={name}");

            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsJsonAsync<RoomsDTO>();
                return message;
            }

            return null;
        }

        public async Task<IEnumerable<RoomsDTO>> GetRoomsAsync()
        {
            var response = await _client.GetAsync(nameof(RoomsController.GetRooms));

            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsJsonAsync<List<RoomsDTO>>();
                return message;
            }

            return Enumerable.Empty<RoomsDTO>();
        }

        public async Task RemoveRoomAsync(string name)
        {
            await _client.DeleteAsJsonAsync(nameof(RoomsController.RemoveRoom), name);
        }

        public async Task<string> CreateRoomAsync(RoomsDTO roomData)
        {
            var response = await _client.PostAsJsonAsync(nameof(RoomsController.CreateRoom), roomData);

            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                return message;
            }

            return null;
        }
    }
}