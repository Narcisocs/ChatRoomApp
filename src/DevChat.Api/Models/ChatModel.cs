using DevChat.Business.Models;

namespace DevChat.Api.Models
{
    public class ChatModel
    {
        public List<ChatRoom> ChatRooms { get; set; }
        public User User { get; set; }
    }
}
