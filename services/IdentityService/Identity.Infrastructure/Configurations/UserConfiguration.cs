using System;
using System.Collections.Generic;
using System.Text;

using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.PasswordHash)
            .IsRequired();

        builder.Property(x => x.ProfilePicture)
            .HasMaxLength(500);

        builder.Property(x => x.EmailVerificationToken)
    .HasMaxLength(256);

        builder.Property(x => x.EmailVerificationTokenExpiryTime);

        builder.Property(x => x.PasswordResetToken)
    .HasMaxLength(256);

        builder.Property(x => x.PasswordResetTokenExpiryTime);
    }
}