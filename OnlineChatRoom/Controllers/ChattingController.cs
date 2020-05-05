using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineChatRoom.DataAccess.Models;
using OnlineChatRoom.ViewModels;

namespace OnlineChatRoom.Controllers
{
    public class ChattingController : Controller
    {
        private readonly UserManager<AspNetUsers> _userManager;

        public ChattingController(UserManager<AspNetUsers> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetPartialUserMessage(string username, string msg, DateTime timestamp)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                return PartialView("~/Views/Chatting/_UserMessagePartial.cshtml",
                    new UserMessageViewModel {User = user, Message = msg, Timestamp = timestamp });
            }

            throw new InvalidOperationException("This operation requires a user!");
        }

        [HttpGet]
        public IActionResult GetPartialSystemMessage(string msg, DateTime timestamp)
        {
            return PartialView("~/Views/Chatting/_SystemMessagePartial.cshtml",
                new SystemMessageViewModel {Message = msg, Timestamp = timestamp});
        }
    }
}