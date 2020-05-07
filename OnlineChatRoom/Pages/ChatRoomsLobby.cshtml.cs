using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineChatRoom.Pages
{
    public class ChatRoomsLobbyModel : PageModel
    {
        [BindProperty]
        public string ExistingRoomName { get; set; }

        [BindProperty]
        public string NewRoomName { get; set; }
     
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostJoinExisting()
        {
            if (string.IsNullOrEmpty(ExistingRoomName))
            {
                ModelState.AddModelError(string.Empty, "A room name is required.");
                return Page();
            }
            //TODO check if exists
            return RedirectToAction("JoinChatRoom", "Chatting", routeValues: new {roomName = ExistingRoomName});
        }

        public async Task<IActionResult> OnPostCreateNew()
        {
            if (string.IsNullOrEmpty(NewRoomName))
            {
                ModelState.AddModelError(string.Empty, "A room name is required.");
                return Page();
            }
            //TODo check if exists
            return RedirectToAction("JoinChatRoom", "Chatting", routeValues: new { roomName = NewRoomName });
        }
    }
}