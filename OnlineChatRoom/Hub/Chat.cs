using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OnlineChatRoom.Common.DTOs;
using OnlineChatRoom.IServices;

namespace OnlineChatRoom.Hub
{
    public class Chat : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly ILoggerService _loggerService;

        public Chat(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task JoinRoom(string roomName)
        {
            try
            {
                //Context.GetHttpContext().Request.Headers["User-Agent"];
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
