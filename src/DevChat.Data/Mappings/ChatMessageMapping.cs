using DevChat.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevChat.Data.Mappings
{
    public class ChatMessageMapping : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Content)
                .IsRequired()
                .HasColumnType("varchar(1500)");

            builder.Property(p => p.CreatedDate)
                .IsRequired()
                .HasColumnType("DateTime");

            builder.ToTable("ChatMessages");
        }
    }
}
