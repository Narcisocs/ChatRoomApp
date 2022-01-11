namespace DevChat.MVC.Models.DTOS
{
    public class CreateRoomDTO
    {
        public const string BindProperties = "ChatRoomName";

        public string ChatRoomName { get; set; }
    }
}
