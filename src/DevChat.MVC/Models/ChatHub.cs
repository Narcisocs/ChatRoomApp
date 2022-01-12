using DevChat.Business.Interfaces;
using DevChat.Business.Models;
using Microsoft.AspNetCore.SignalR;

namespace DevChat.MVC.Models
{
    public class ChatHub : Hub
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly IUserService _userService;

        public ChatHub(IChatRoomService chatRoomService, IUserService userService)
        {
            _chatRoomService = chatRoomService;
            _userService = userService;
    }

        public async Task Send(string chatroom, string user, string message)
        {
            var chatRoomDB = await _chatRoomService.GetById(Convert.ToInt64(chatroom.Replace("room", "")));

            var userDB = await _userService.GetById(Convert.ToInt64(user));

            var chatMessage = new ChatMessage() { Content = message, ChatRoom = chatRoomDB, User = userDB };

            await _chatRoomService.Add(chatMessage);

            await Clients.All.SendAsync(chatroom, userDB.Name, message);
        }
    }
}
