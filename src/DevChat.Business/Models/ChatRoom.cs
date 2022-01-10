namespace DevChat.Business.Models
{
    public  class ChatRoom : Entity
    {
        public string Name { get; set; }

        /* EF Relations */
        public IEnumerable<ChatMessage> ChatMessages { get; set; }
        public ICollection<Participant> Participants { get; set; }
    }
}
