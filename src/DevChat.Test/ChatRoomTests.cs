using DevChat.Business.Interfaces;
using DevChat.Business.Models;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevChat.Test
{
    public class ChatRoomTests
    {
        private IChatRoomService _chatRoomService;
        private IUserService _userService;

        [SetUp]
        public void Setup(IChatRoomService chatRoomService, IUserService userService)
        {
            _chatRoomService = chatRoomService;
            _userService = userService;
        }

        [Test]
        public async Task CreateRoom()
        {
            //Arrange
            var chatRoom = new ChatRoom()
            {
                Name = "Room" + new Random().Next(0, 100)
            };

            //Act
            await _chatRoomService.Add(chatRoom);
            var listOfRooms = await _chatRoomService.GetAll();

            //Arrange
            Assert.Contains(chatRoom, listOfRooms);
        }

        [Test]
        public async Task EnterRoom()
        {
            //Arrange
            var chatRoom = new ChatRoom()
            {
                Name = "Room" + new Random().Next(0, 100)
            };

            var user = new User()
            {
                Name = "Charles",
                Email = "charles@gmail.com",
                Phone = "1234556",
                Password = "123456"
            };

            //Act
            await _userService.Add(user);
            await _chatRoomService.Add(chatRoom);

            var chatRoomDB = (await _chatRoomService.GetAll()).First(c => c.Name == chatRoom.Name);
            
            var userDB = await _userService.FindByEmail(user.Email);
            await _userService.Join(chatRoomDB, userDB);

            //Arrange
            Assert.Contains(user, chatRoomDB.Participants.Select(p => p.User).ToList());
        }

        [Test]
        public async Task LeaveRoom()
        {
            //Arrange
            var chatRoom = new ChatRoom()
            {
                Name = "Room" + new Random().Next(0, 100)
            };

            var user = new User()
            {
                Name = "Charles",
                Email = "charles@gmail.com",
                Phone = "1234556",
                Password = "123456"
            };

            //Act
            await _userService.Add(user);
            await _chatRoomService.Add(chatRoom);

            var chatRoomDB = (await _chatRoomService.GetAll()).First(c => c.Name == chatRoom.Name);

            var userDB = await _userService.FindByEmail(user.Email);
            await _userService.Leave(chatRoomDB, userDB);

            //Arrange
            Assert.IsFalse(chatRoomDB.Participants.Any(p => p.User.Email == user.Email));
        }
    }
}
