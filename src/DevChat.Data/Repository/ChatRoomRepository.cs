using DevChat.Business.Interfaces;
using DevChat.Business.Models;
using DevChat.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevChat.Data.Repository
{
    public class ChatRoomRepository : Repository<ChatRoom>, IChatRoomRepository
    {
        public ChatRoomRepository(MyDbContext context) : base(context) { }

        public async Task CreateRoom(string chatRoomName)
        {
            var chatRoom = new ChatRoom() { Name = chatRoomName };

            Db.ChatRooms.Add(chatRoom);
            
            await SaveChanges();
        }

        public async Task<IEnumerable<ChatMessage>> ListMessages(ChatRoom chatRoom)
        {
            int messageLimit = 50;

            return await Db.ChatMessages.AsNoTracking()
                .Where(c => c.ChatRoom.Id == chatRoom.Id)
                .Include(c => c.User)
                .OrderByDescending(m => m.CreatedDate)
                .Take(messageLimit)
                .OrderBy(m => m.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> ListParticipants(ChatRoom chatRoom)
        {
            return await Db.ChatRooms.AsNoTracking()
                .Where(c => c.Id == chatRoom.Id)
                .Include(c => c.Participants)
                .ThenInclude(row => row.User)
                .SelectMany(c => c.Participants.Where(p => p.ChatRoomId == chatRoom.Id).Select(p => p.User))
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

        public async Task Add(ChatMessage message)
        {
            Db.ChatMessages.Add(message);

            await SaveChanges();
        }
    }
}
