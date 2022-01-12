using DevChat.Business.Models;

namespace DevChat.Business.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> Login(User user);
        Task<User> FindByEmail(string email);
        Task Join(ChatRoom chatRoom, User user);
        Task Leave(ChatRoom chatRoom, User user);
        Task SendMessageTo(ChatRoom chatRoom, string message);
        Task<IEnumerable<Participant>> ListParticipantsOf(ChatRoom chatRoom);
        Task<IEnumerable<ChatMessage>> ListMessagesOf(ChatRoom chatRoom, User user);
    }
}
