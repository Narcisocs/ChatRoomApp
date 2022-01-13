using DevChat.Api.DTOS;
using DevChat.Business.Interfaces;
using DevChat.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevChat.Api.Controllers
{
    [ApiController]
    [Route("/api/Auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Signup")]
        public async Task<JsonResult> SignUp([Bind(include: UserDTO.BindProperties)] UserDTO user)
        {
            var userRegister = new User() { Name = user.Name, Email = user.Email, Phone = user.Phone, Password = user.Password };

            var operationResult = new OperationResult();

            try
            {
                await _userService.Add(userRegister);

                operationResult.StatusCode = StatusCode(200).StatusCode;
                operationResult.Success = "true";
                operationResult.Entity = user;
            }
            catch (Exception ex)
            {
                operationResult.StatusCode = StatusCode(500).StatusCode;
                operationResult.Message = ex.Message;
            }

            return new JsonResult(operationResult);
        }
    }
}
