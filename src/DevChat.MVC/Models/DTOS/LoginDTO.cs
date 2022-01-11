namespace DevChat.MVC.Models.DTOS
{
    public class LoginDTO
    {
        public const string BindProperties = "Email,Password";

        public string Email { get; set; } 
        public string Password { get; set; }
    }
}
