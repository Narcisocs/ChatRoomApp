namespace DevChat.Business.Models
{
    public class ChatMessage : Entity
    {
        public string Content { get; set; }

        /* EF Relations */
        public ChatRoom ChatRoom { get; set; }
        public User User { get; set; }
    }
}
