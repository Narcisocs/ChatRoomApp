using DevChat.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevChat.Data.Mappings
{
    public class ParticipantMapping : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.HasKey(p => new { p.ChatRoomId, p.UserId });

            builder.HasOne(c => c.ChatRoom)
                .WithMany(p => p.Participants)
                .HasForeignKey(c => c.ChatRoomId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(u => u.User)
                .WithMany(p => p.Participants)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
