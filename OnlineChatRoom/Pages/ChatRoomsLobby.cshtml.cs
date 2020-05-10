using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineChatRoom.Common.DTO;
using OnlineChatRoom.IServices;

namespace OnlineChatRoom.Pages
{
    public class ChatRoomsLobbyModel : PageModel
    {
        private readonly ILoggerService _loggerService;
        private readonly IRoomsService _roomsService;

        [BindProperty]
        public string ExistingRoomName { get; set; }

        [BindProperty]
        public string NewRoomName { get; set; }

        public List<RoomsDTO> PopularRooms { get; set; }

        public ChatRoomsLobbyModel(ILoggerService loggerService, IRoomsService roomsService)
        {
            _loggerService = loggerService;
            _roomsService = roomsService;
        }

        public async Task OnGet()
        {
            var rooms = await _roomsService.GetRoomsAsync();
            PopularRooms = new List<RoomsDTO>(rooms.OrderByDescending(dto => dto.ChatConnections.Count));
        }

        public async Task<IActionResult> OnPostJoinExisting()
        {
            if (string.IsNullOrEmpty(ExistingRoomName))
            {
                ModelState.AddModelError(string.Empty, "A room name is required.");
                return Page();
            }

            var room = await _roomsService.GetRoomAsync(ExistingRoomName);
            if (room != null)
            {
                return RedirectToAction("JoinChatRoom", "Chatting", routeValues: new { roomName = ExistingRoomName });
            }

            ModelState.AddModelError(string.Empty, "Such room doesn't exist :/");
            return Page();
        }

        public async Task<IActionResult> OnPostCreateNew()
        {
            if (string.IsNullOrEmpty(NewRoomName))
            {
                ModelState.AddModelError(string.Empty, "A room name is required.");
                return Page();
            }
            var room = await _roomsService.GetRoomAsync(NewRoomName);
            if (room == null)
            {
                await _roomsService.CreateRoomAsync(new RoomsDTO {RoomName = NewRoomName});
                return RedirectToAction("JoinChatRoom", "Chatting", routeValues: new { roomName = NewRoomName });
            }

            ModelState.AddModelError(string.Empty, "Such room already exist :/");
            return Page();
        }
    }
}