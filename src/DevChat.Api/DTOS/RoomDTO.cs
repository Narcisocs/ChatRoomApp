namespace DevChat.Api.DTOS
{
    public class RoomDTO
    {
        public const string BindProperties = "RoomId";

        public long RoomId { get; set; }
        public string userEmail { get; set; }
    }
}
