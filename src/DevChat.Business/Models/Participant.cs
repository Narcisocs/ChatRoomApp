using System;
namespace DevChat.Business.Models
{
    public class Participant
    {
        public long ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}
