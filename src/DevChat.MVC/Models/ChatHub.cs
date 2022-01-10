using Microsoft.AspNetCore.SignalR;

namespace DevChat.MVC.Models
{
    public class ChatHub : Hub
    {
        public async Task Send(string chatroom, string user, string message)
        {
            await Clients.All.SendAsync(chatroom, user, message);
        }
    }
}
