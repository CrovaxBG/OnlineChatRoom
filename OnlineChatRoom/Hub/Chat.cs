using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using OnlineChatRoom.Common.DTO;
using OnlineChatRoom.DataAccess.Models;
using OnlineChatRoom.IServices;

namespace OnlineChatRoom.Hub
{
    public class Chat : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly ILoggerService _loggerService;
        private readonly IChatConnectionsService _chatConnectionsService;
        private readonly IRoomsService _roomsService;
        private readonly UserManager<AspNetUsers> _userManager;

        public Chat(
            ILoggerService loggerService,
            IChatConnectionsService chatConnectionsService,
            IRoomsService roomsService,
            UserManager<AspNetUsers> userManager)
        {
            _loggerService = loggerService;
            _chatConnectionsService = chatConnectionsService;
            _roomsService = roomsService;
            _userManager = userManager;
        }

        public override async Task OnConnectedAsync()
        {
            var currentUser = await _userManager.FindByNameAsync(Context.User.Identity.Name);
            await _chatConnectionsService.CreateChatConnectionAsync(new ChatConnectionsDTO
            {
                ConnectionId = Context.ConnectionId, UserId = currentUser.Id,
                UserAgent = Context.GetHttpContext().Request.Headers["User-Agent"]
            });
            await base.OnConnectedAsync();
        }

        public async Task JoinRoom(string roomName)
        {
            try
            {
                var currentConnection = await _chatConnectionsService.GetChatConnectionAsync(Context.ConnectionId);
                await _chatConnectionsService.UpdateChatConnectionAsync(new ChatConnectionsDTO
                {
                    ConnectionId = Context.ConnectionId, RoomName = roomName, UserId = currentConnection.UserId,
                    UserAgent = currentConnection.UserAgent, User = currentConnection.User, RoomNameNavigation = currentConnection.RoomNameNavigation
                });

                var rooms = await _roomsService.GetRoomAsync(roomName);
                await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            }
            catch (Exception e)
            {
                await _loggerService.LogAsync(new LogDTO
                    {Date = DateTime.Now, Message = e.Message, StackTrace = e.StackTrace});
            }
        }

        public async Task LeaveRoom(string roomName)
        {
            try
            {
                 await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            }
            catch (Exception e)
            {
                await _loggerService.LogAsync(new LogDTO
                    { Date = DateTime.Now, Message = e.Message, StackTrace = e.StackTrace });
            }
        }

        public async Task BroadcastMessage(string roomName, string name, string message)
        {
            try
            {
                await Clients.Group(roomName).SendAsync("broadcastMessage", name, message);
            }
            catch (Exception e)
            {
                await _loggerService.LogAsync(new LogDTO
                    { Date = DateTime.Now, Message = e.Message, StackTrace = e.StackTrace });
            }
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
