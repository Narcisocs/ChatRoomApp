namespace DevChat.MVC.Models.DTOS
{
    public class UserDTO
    {
        public const string BindProperties = "Name,Email,Phone,Password";

        public string Name { get; set; } 
        public string Email { get; set; } 
        public string Phone { get; set; } 
        public string Password {get; set; }
    }
}
