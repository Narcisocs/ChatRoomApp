using System.Text.Json.Serialization;

namespace DevChat.Business.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        /* EF Relations */
        [JsonIgnore]
        public IEnumerable<ChatMessage> chatMessages { get; set; }
        [JsonIgnore]
        public ICollection<Participant> Participants { get; set; }
    }
}
