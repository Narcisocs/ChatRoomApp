using DevChat.Business.Interfaces;
using DevChat.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevChat.MVC.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatRoomRepository _chatRoomRepository;

        public ChatController(IChatRoomRepository chatRoomRepository)
        {
            _chatRoomRepository = chatRoomRepository;
        }

        public async Task<IActionResult> Index()
        {
            var chatRooms = await _chatRoomRepository.GetAll();
            var model = new ChatModel() { ChatRooms = chatRooms };

            return View(model);
        }

        public async Task<IActionResult> Chat()
        {
            var chatRooms = await _chatRoomRepository.GetAll();
            var model = new ChatModel() { ChatRooms = chatRooms };

            return View(model);
        }
    }
}