﻿namespace DevChat.Business.Models
{
    public class Participant : Entity
    {
        /* EF Relations */
        public IEnumerable<ChatRoom> ChatRoom { get; set; }
        public IEnumerable<User> user { get; set; }
    }
}