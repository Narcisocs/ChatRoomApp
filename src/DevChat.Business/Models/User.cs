namespace DevChat.Business.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        /* EF Relations */
        public IEnumerable<ChatMessage> chatMessages { get; set; }
        public ICollection<Participant> Participants { get; set; }
    }
}
