using DevChat.Business.Interfaces;
using DevChat.Business.Models;

namespace DevChat.Business.Services
{
    public class ChatRoomService : BaseService<ChatRoom>, IChatRoomService
    {
        private readonly IChatRoomRepository _chatRoomRepository;

        public ChatRoomService(IChatRoomRepository ChatRoomRepository) : base(ChatRoomRepository)
        {
            _chatRoomRepository = ChatRoomRepository;
        }

        public async Task Add(ChatRoom chatRoom, User user)
        {
            await _chatRoomRepository.Add(chatRoom, user);
        }

        public async Task Add(ChatRoom chatRoom, ChatMessage message)
        {
            await _chatRoomRepository.Add(chatRoom, message);
        }

        public async Task CreateRoom(string chatRoomName)
        {
            await _chatRoomRepository.CreateRoom(chatRoomName);
        }

        public async Task<IEnumerable<ChatMessage>> ListMessages(ChatRoom chatRoom)
        {
            return await _chatRoomRepository.ListMessages(chatRoom);
        }

        public async Task<IEnumerable<User>> ListParticipants(ChatRoom chatRoom)
        {
            return await _chatRoomRepository.ListParticipants(chatRoom);
        }

        public async Task Remove(ChatRoom chatRoom, User user)
        {
            await _chatRoomRepository.Remove(chatRoom, user);
        }
    }
}
