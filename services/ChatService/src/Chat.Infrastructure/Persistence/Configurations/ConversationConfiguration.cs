using Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infrastructure.Persistence.Configurations;

public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type)
               .IsRequired();

        builder.Property(x => x.Name)
               .HasMaxLength(200);

        builder.Property(x => x.CreatedBy)
               .IsRequired();

        builder.Property(x => x.CreatedAt)
               .IsRequired();

        builder.HasMany(x => x.Participants)
               .WithOne(x => x.Conversation)
               .HasForeignKey(x => x.ConversationId);

        builder.HasMany(x => x.Messages)
               .WithOne(x => x.Conversation)
               .HasForeignKey(x => x.ConversationId);
    }
}