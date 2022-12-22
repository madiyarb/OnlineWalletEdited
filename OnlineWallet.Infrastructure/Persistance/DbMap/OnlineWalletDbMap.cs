using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineWallet.Domain.Entities;

namespace OnlineWallet.Infrastructure.Persistance.DbMap;

public class OnlineWalletDbMap : IEntityTypeConfiguration<OnlineWalletDbModel>
{
    public void Configure(EntityTypeBuilder<OnlineWalletDbModel> builder)
    {
        builder.Property(p => p.CreatedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("now()").IsRequired();
        builder.Property(p => p.LastModifiedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("now()").IsRequired();
        builder.Property(p => p.WalletUnicode).HasColumnType("VARCHAR(36)").IsRequired();
        builder.Property(p => p.Amount).HasColumnType("DECIMAL").IsRequired();
    }
}