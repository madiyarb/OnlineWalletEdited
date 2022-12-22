using Identity.Domain.Entities;
using identity.Infrastructure.Persistance.DbMap;
using Microsoft.EntityFrameworkCore;
namespace identity.Infrastructure.Persistance;

public class IdentityDbContext : DbContext
{
    public DbSet<UserDbModel> Users { get; set; }
    

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UserDbMap());
    }
}