using Microsoft.EntityFrameworkCore;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Infrastructure.Persistance.DbMap;

namespace OnlineWallet.Infrastructure.Persistance;

public class OnlineWalletDbContext : DbContext
{
    public DbSet<OnlineWalletDbModel> OnlineWallets { get; set; }
    
    public OnlineWalletDbContext(DbContextOptions<OnlineWalletDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new OnlineWalletDbMap());
    }
}