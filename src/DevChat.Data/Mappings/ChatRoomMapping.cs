using DevChat.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevChat.Data.Mappings
{
    public class ChatRoomMapping : IEntityTypeConfiguration<ChatRoom>
    {
        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(p => p.CreatedDate)
                .IsRequired()
                .HasColumnType("DateTime");

            builder.ToTable("ChatRooms");
        }
    }
}
