using DevChat.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevChat.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(p => p.Email)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.Phone)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(p => p.Password)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(p => p.CreatedDate)
                .IsRequired()
                .HasColumnType("DateTime");

            builder.ToTable("Users");
        }
    }
}
