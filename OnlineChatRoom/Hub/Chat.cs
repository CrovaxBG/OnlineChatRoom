using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace OnlineChatRoom.Hub
{
    public class Chat : Microsoft.AspNetCore.SignalR.Hub
    {
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
                //TODO add logging
            }
        }

        public Task LeaveRoom(string roomName)
        {
            try
            {
                return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            }
            catch (Exception e)
            {
                //TODO add logging
                return null;
            }
        }

        public void BroadcastMessage(string roomName, string name, string message)
        {
            try
            {
                Clients.Group(roomName).SendAsync("broadcastMessage", name, message);
            }
            catch (Exception e)
            {
                //TODO add logging
            }
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
