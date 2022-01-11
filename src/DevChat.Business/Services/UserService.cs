using DevChat.Business.Interfaces;
using DevChat.Business.Models;

namespace DevChat.Business.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        public async Task<bool> Login(User user)
        {
            return await _userRepository.Login(user);
        }

        public async Task<User> FindByEmail(string email)
        {
            return await _userRepository.FindByEmail(email);
        }

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Join(ChatRoom chatRoom, User user)
        {
            await _userRepository.Join(chatRoom, user);
        }

        public async Task Leave(ChatRoom chatRoom, User user)
        {
            await _userRepository.Leave(chatRoom, user);
        }

        public async Task<IEnumerable<ChatMessage>> ListMessagesOf(ChatRoom chatRoom, User user)
        {
            return await _userRepository.ListMessagesOf(chatRoom, user);
        }

        public async Task<IEnumerable<User>> ListParticipantsOf(ChatRoom chatRoom)
        {
            return await _userRepository.ListParticipantsOf(chatRoom);
        }

        public async Task SendMessageTo(ChatRoom chatRoom, string message)
        {
            await SendMessageTo(chatRoom, message);
        }
    }
}
