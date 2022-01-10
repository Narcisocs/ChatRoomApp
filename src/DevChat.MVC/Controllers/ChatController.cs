using DevChat.Business.Interfaces;
using DevChat.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevChat.MVC.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatRoomService _chatRoomService;

        public ChatController(IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await GetAllChatRooms();

            return View(model);
        }

        public IActionResult CreateRoom()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateRoom(string chatRoomName)
        {
            await _chatRoomService.CreateRoom(chatRoomName);

            TempData["message"] = "A room was created";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EnterRoom(long roomId)
        {
            var chatRoom = await _chatRoomService.GetById(roomId);

            return View("chat", new ChatModel() { ChatRooms = new List<Business.Models.ChatRoom>() { chatRoom } });
        }

        private async Task<ChatModel> GetAllChatRooms()
        {
            var chatRooms = await _chatRoomService.GetAll();
            var model = new ChatModel() { ChatRooms = chatRooms };

            return model;
        }
    }
}