using DevChat.Business.Models;

namespace DevChat.Business.Interfaces
{
    public  interface IUserService : IBaseService<User>, IDisposable
    {
        Task<bool> Login(User user);
        Task<User> FindByEmail(string email);
        Task Join(ChatRoom chatRoom, User user);
        Task Leave(ChatRoom chatRoom, User user);
        Task SendMessageTo(ChatRoom chatRoom, string message);
        Task<IEnumerable<User>> ListParticipantsOf(ChatRoom chatRoom);
        Task<IEnumerable<ChatMessage>> ListMessagesOf(ChatRoom chatRoom, User user);
    }
}
