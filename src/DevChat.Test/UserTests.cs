using DevChat.Business.Interfaces;
using DevChat.Business.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DevChat.Test
{
    public class UserTests
    {
        private IUserService _userService;

        [SetUp]
        public void Setup(IUserService userService)
        {
            _userService = userService;
        }

        [Test]
        public async Task SignUp()
        {
            //Arrange
            var user = new User()
            {
                Name = "Charles",
                Email = "charles@gmail.com",
                Phone = "1234556",
                Password = "123456"
            };

            //Act
            await _userService.Add(user);
            var userDB = await _userService.FindByEmail(user.Email);

            //Arrange
            Assert.AreEqual(userDB.Name, user.Name);
            Assert.AreEqual(userDB.Email, user.Email);
            Assert.AreEqual(userDB.Phone, user.Phone);
            Assert.AreEqual(userDB.Password, user.Password);
        }

        [Test]
        public async Task Login()
        {
            //Arrange
            var user = new User()
            {
                Name = "Matthew",
                Email = "matthew@gmail.com",
                Phone = "654321",
                Password = "654321"
            };

            //Act
            await _userService.Add(user);
            var userDB = await _userService.FindByEmail(user.Email);

            //Arrange
            Assert.AreEqual(userDB.Name, user.Name);
            Assert.AreEqual(userDB.Email, user.Email);
            Assert.AreEqual(userDB.Phone, user.Phone);
            Assert.AreEqual(userDB.Password, user.Password);
        }
    }
}
