using DevChat.Api.DTOS;
using DevChat.Api.Models;
using DevChat.Business.Interfaces;
using DevChat.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevChat.Api.Controllers
{
    [ApiController]
    [Route("/api/Chat")]
    public class ChatController : ControllerBase
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly IUserService _userService;

        public ChatController(IChatRoomService chatRoomService, IUserService userService)
        {
            _chatRoomService = chatRoomService;
            _userService = userService;
        }

        [HttpGet("list-of-all-rooms")]
        public async Task<JsonResult> ListOfAllRooms()
        {
            var operationResult = new OperationResult();

            try
            {
                var model = await GetAllChatRooms();

                operationResult.StatusCode = StatusCode(200).StatusCode;
                operationResult.Success = "true";
                operationResult.Entity = model;
            }
            catch (Exception ex)
            {
                operationResult.StatusCode = StatusCode(500).StatusCode;
                operationResult.Message = ex.Message;
            }

            return new JsonResult(operationResult);
        }

        [HttpPost("create-room")]
        public async Task<IActionResult> CreateRoom([Bind(include: CreateRoomDTO.BindProperties)] CreateRoomDTO room)
        {
            var operationResult = new OperationResult();

            try
            {
                await _chatRoomService.CreateRoom(room.ChatRoomName);

                operationResult.StatusCode = StatusCode(200).StatusCode;
                operationResult.Success = "true";
                operationResult.Entity = room;
            }
            catch (Exception ex)
            {
                operationResult.StatusCode = StatusCode(500).StatusCode;
                operationResult.Message = ex.Message;
            }

            return new JsonResult(operationResult);
        }

        [HttpPost("enter-room")]
        public async Task<IActionResult> EnterRoom([Bind(include: RoomDTO.BindProperties)] RoomDTO room)
        {
            var operationResult = new OperationResult();

            try
            {
                var chatRoom = await _chatRoomService.GetById(room.RoomId);

                var usersInRoom = await _chatRoomService.ListParticipants(chatRoom);

                var user = await _userService.FindByEmail(room.userEmail);

                if (!usersInRoom.Any(u => u.Id == user.Id))
                    await _userService.Join(chatRoom, user);

                operationResult.StatusCode = StatusCode(200).StatusCode;
                operationResult.Success = "true";
                operationResult.Entity = new ChatModel() { ChatRooms = new List<ChatRoom>() { chatRoom }, User = user };
            }
            catch (Exception ex)
            {
                operationResult.StatusCode = StatusCode(500).StatusCode;
                operationResult.Message = ex.Message;
            }

            return new JsonResult(operationResult);
        }

        [HttpPost("leave-room")]
        public async Task<IActionResult> LeaveRoom([Bind(include: RoomDTO.BindProperties)] RoomDTO room)
        {
            var operationResult = new OperationResult();

            try
            {
                var chatRoom = await _chatRoomService.GetById(room.RoomId);

                var user = await _userService.FindByEmail(room.userEmail);

                await _userService.Leave(chatRoom, user);

                operationResult.StatusCode = StatusCode(200).StatusCode;
                operationResult.Success = "true";
                operationResult.Entity = new ChatModel() { ChatRooms = new List<ChatRoom>() { chatRoom }, User = user };
            }
            catch (Exception ex)
            {
                operationResult.StatusCode = StatusCode(500).StatusCode;
                operationResult.Message = ex.Message;
            }

            return new JsonResult(operationResult);
        }
        private async Task<ChatModel> GetAllChatRooms()
        {
            var chatRooms = await _chatRoomService.GetAll();

            foreach (var chatRoom in chatRooms)
            {
                var participants = await _userService.ListParticipantsOf(chatRoom);
                chatRoom.Participants = participants.ToList();
            }

            var model = new ChatModel() { ChatRooms = chatRooms };

            return model;
        }
    }
}
