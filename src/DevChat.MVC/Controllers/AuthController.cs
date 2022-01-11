using DevChat.Business.Interfaces;
using DevChat.Business.Models;
using DevChat.MVC.Models.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace DevChat.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind(include: LoginDTO.BindProperties)] LoginDTO login)
        {
            if (login == null)
            {
                return BadRequest();
            }

            var user = await _userService.FindByEmail(login.Email);

            if(user == null)
            {
                return NotFound();
            }

            if (await _userService.Login(user))
            {
                await AddUserToSession(user.Email);

                return RedirectToAction("Index", "Chat");
            }

            TempData["message"] = "Email or Password invalid.";

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind(include: UserDTO.BindProperties)] UserDTO user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var userRegister = new User() { Name = user.Name, Email = user.Email, Phone = user.Phone, Password = user.Password };

            await _userService.Add(userRegister);

            await AddUserToSession(userRegister.Email);

            return RedirectToAction("Index", "Chat");
        }

        private async Task AddUserToSession(string email)
        {
            var userDB = await _userService.FindByEmail(email);

            if (userDB == null)
                return;

            HttpContext.Session.SetString("user", userDB.Email);
            HttpContext.Session.SetString("userID", userDB.Id.ToString());
        }
    }
}
