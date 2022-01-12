using System.Text.Json.Serialization;

namespace DevChat.Business.Models
{
    public  class ChatRoom : Entity
    {
        public string Name { get; set; }

        /* EF Relations */
        [JsonIgnore]
        public IEnumerable<ChatMessage> ChatMessages { get; set; }
        [JsonIgnore]
        public ICollection<Participant> Participants { get; set; }
    }
}
