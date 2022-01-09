﻿using DevChat.Business.Models;

namespace DevChat.Business.Interfaces
{
    public interface IUserRepository
    {
        Task Join(ChatRoom chatRoom, User user);
        Task Leave(ChatRoom chatRoom, User user);
        Task SendMessageTo(ChatRoom chatRoom, string message);
        Task<IEnumerable<User>> ListParticipantsOf(ChatRoom chatRoom);
        Task<IEnumerable<ChatMessage>> ListMessagesOf(ChatRoom chatRoom, User user);
    }
}