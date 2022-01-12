using DevChat.Business.Interfaces;
using DevChat.Business.Models;
using DevChat.MVC.Models;
using DevChat.MVC.Models.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace DevChat.MVC.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly IUserService _userService;

        public ChatController(IChatRoomService chatRoomService, IUserService userService)
        {
            _chatRoomService = chatRoomService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await GetAllChatRooms();

            var user = await GetLoggedUser();

            ViewBag.User = user;

            return View(model);
        }

        public async Task<IActionResult> CreateRoom()
        {
            ViewBag.User = await GetLoggedUser();

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateRoom([Bind(include: CreateRoomDTO.BindProperties)] CreateRoomDTO room)
        {
            await _chatRoomService.CreateRoom(room.ChatRoomName);

            ViewBag.User = await GetLoggedUser();

            TempData["message"] = "A room was created";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EnterRoom([Bind(include: EnterRoomDTO.BindProperties)] EnterRoomDTO room)
        {
            var chatRoom = await _chatRoomService.GetById(room.RoomId);

            var chatMessages = await _chatRoomService.ListMessages(chatRoom);
            ViewBag.Messages = chatMessages;

            var usersInRoom = await _chatRoomService.ListParticipants(chatRoom);

            var user = await GetLoggedUser();
            ViewBag.User = user;

            if (!usersInRoom.Any(u => u.Id == user.Id))
                await _userService.Join(chatRoom, user);

            return View("ChatRoom", new ChatModel() { ChatRooms = new List<ChatRoom>() { chatRoom }, User = user });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> LeaveRoom([Bind(include: EnterRoomDTO.BindProperties)] EnterRoomDTO room)
        {
            var chatRoom = await _chatRoomService.GetById(room.RoomId);

            var user = await GetLoggedUser();

            await _userService.Leave(chatRoom, user);

            return RedirectToAction("Index");
        }

        private async Task<ChatModel> GetAllChatRooms()
        {
            var chatRooms = await _chatRoomService.GetAll();

            chatRooms.ForEach(async chatRoom => {
                var participants = await _userService.ListParticipantsOf(chatRoom);
                chatRoom.Participants = participants.ToList();
            });

            var model = new ChatModel() { ChatRooms = chatRooms };

            return model;
        }

        private async Task<User> GetLoggedUser()
        {
            var userID = HttpContext.Session.GetString("userID");

            var user = await _userService.GetById(Convert.ToInt64(userID));

            return user;
        }
    }
}