using DevChat.Business.Interfaces;
using DevChat.Business.Models;
using DevChat.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevChat.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MyDbContext context) : base(context) { }

        public async Task Join(ChatRoom chatRoom, User user)
        {
            var users = await ListParticipantsOf(chatRoom);

            users.ToList().Add(user);

            await SaveChanges();
        }

        public async Task Leave(ChatRoom chatRoom, User user)
        {
            var users = await ListParticipantsOf(chatRoom);

            users.ToList().Add(user);

            await SaveChanges();
        }

        public async Task<IEnumerable<ChatMessage>> ListMessagesOf(ChatRoom chatRoom, User user)
        {
            return await Db.Users.AsNoTracking()
                .Where(u => u.Participants.Any(p => p.ChatRoomId == chatRoom.Id))
                .SelectMany(c => c.chatMessages)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> ListParticipantsOf(ChatRoom chatRoom)
        {
            return await Db.Users.AsNoTracking()
                .Where(u => u.Participants.Any(p => p.ChatRoomId == chatRoom.Id))
                .ToListAsync();
        }

        public async Task SendMessageTo(ChatRoom chatRoom, string message)
        {
            var messages = await Db.Users.AsNoTracking()
                .Where(u => u.Participants.Any(p => p.ChatRoomId == chatRoom.Id))
                .SelectMany(c => c.chatMessages)
                .ToListAsync();

            messages.ToList().Add(new ChatMessage() { Content = message });
            
            await SaveChanges();
        }
    }
}
