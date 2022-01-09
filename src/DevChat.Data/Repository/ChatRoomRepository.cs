using DevChat.Business.Interfaces;
using DevChat.Business.Models;
using DevChat.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevChat.Data.Repository
{
    public class ChatRoomRepository : Repository<ChatRoom>, IChatRoomRepository
    {
        public ChatRoomRepository(MyDbContext context) : base(context) { }

        public async Task<IEnumerable<ChatMessage>> ListMessages(ChatRoom chatRoom)
        {
            return await Db.ChatRooms.AsNoTracking()
                .Where(c => c.Id == chatRoom.Id)
                .Include(c => c.ChatMessages)
                .SelectMany(c => c.ChatMessages)
                .OrderByDescending(m => m.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> ListParticipants(ChatRoom chatRoom)
        {
            return await Db.ChatRooms.AsNoTracking()
                .Where(c => c.Id == chatRoom.Id)
                .Include(c => c.Users)
                .SelectMany(c => c.Users)
                .ToListAsync();
        }

        public async Task Add(ChatRoom chatRoom, User user)
        {
            var users = await ListParticipants(chatRoom);

            users.ToList().Add(user);

            await SaveChanges();
        }

        public async Task Remove(ChatRoom chatRoom, User user)
        {
            var users = await ListParticipants(chatRoom);

            users.ToList().Remove(user);

            await SaveChanges();
        }

        public async Task Add(ChatRoom chatRoom, ChatMessage message)
        {
            var messages = await ListMessages(chatRoom);

            messages.ToList().Add(message);

            await SaveChanges();
        }
    }
}
