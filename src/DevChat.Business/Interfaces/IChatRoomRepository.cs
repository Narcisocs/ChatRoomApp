using DevChat.Business.Models;

namespace DevChat.Business.Interfaces
{
    public interface IChatRoomRepository : IRepository<ChatRoom>
    {
        Task CreateRoom(string chatRoomName);
        Task Add(ChatRoom chatRoom, User user);
        Task Remove(ChatRoom chatRoom, User user);
        Task<IEnumerable<User>> ListParticipants(ChatRoom chatRoom);
        Task Add(ChatMessage message);
        Task<IEnumerable<ChatMessage>> ListMessages(ChatRoom chatRoom);
    }
}
