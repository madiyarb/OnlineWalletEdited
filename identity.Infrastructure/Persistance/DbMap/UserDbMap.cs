using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace identity.Infrastructure.Persistance.DbMap;

public class UserDbMap : IEntityTypeConfiguration<UserDbModel>
{

    public void Configure(EntityTypeBuilder<UserDbModel> builder)
    {
        builder.Property(p => p.CreatedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("now()").IsRequired();
        builder.Property(p => p.LastModifiedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("now()").IsRequired();
        builder.Property(p => p.Email).HasColumnType("VARCHAR(100)").IsRequired();
        builder.HasIndex(p => p.Email).IsUnique();
        builder.Property(p => p.Password).HasColumnType("VARCHAR(68)").IsRequired();
        builder.Property(p => p.PhoneNumber).HasColumnType("VARCHAR(13)").IsRequired();
        builder.HasIndex(p => p.PhoneNumber).IsUnique();
        builder.Property(p => p.FirstName).HasColumnType("VARCHAR(100)").IsRequired();
    }
}