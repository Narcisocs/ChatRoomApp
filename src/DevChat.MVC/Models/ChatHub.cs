using DevChat.Business.Interfaces;
using DevChat.Business.Models;
using Microsoft.AspNetCore.SignalR;

namespace DevChat.MVC.Models
{
    public class ChatHub : Hub
    {
        private readonly IChatRoomService _chatRoomService;
        
        public ChatHub(IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
        }

        public async Task Send(string chatroom, string user, string message)
        {
            var chatRoomDB = await _chatRoomService.GetById(Convert.ToInt64(chatroom.Replace("room", "")));

            await Clients.All.SendAsync(chatroom, user, message);
        }
    }
}
