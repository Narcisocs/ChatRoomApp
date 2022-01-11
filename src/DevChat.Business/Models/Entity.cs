namespace DevChat.Business.Models
{
    public class Entity
    {
        protected Entity()
        {
            CreatedDate = DateTime.UtcNow;
        }

        public long Id { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
