namespace DevChat.Business.Models
{
    public  class ChatRoom : Entity
    {
        public string Name { get; set; }

        /* EF Relations */
        public IEnumerable<ChatMessage> chatMessages { get; set; }
    }
}
