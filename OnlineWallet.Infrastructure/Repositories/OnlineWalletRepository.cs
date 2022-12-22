using System.Linq.Expressions;
using CommonRepository;
using Microsoft.EntityFrameworkCore;
using OnlineWallet.Application.Contracts;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Infrastructure.Persistance;

namespace OnlineWallet.Infrastructure.Repositories;

public class OnlineWalletRepository : BaseRepository<OnlineWalletDbModel, OnlineWalletDbContext>, IOnlineWalletRepository
{
    public OnlineWalletRepository(OnlineWalletDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<OnlineWalletDbModel> FilterByString(IQueryable<OnlineWalletDbModel> query, string? filterString)
    {
        return string.IsNullOrEmpty(filterString)
            ? query
            : query.Where(v => v.WalletUnicode.ToString().Contains(filterString)
            );
    }
    
    // public async Task<OnlineWalletDbModel?> GetFirstOrDefaultAsyncByUserId(Expression<Func<OnlineWalletDbModel, bool>> predicate, int userId)
    // {
    //     return await Context.Set<OnlineWalletDbModel>()
    //         .Where(p=>p.UserId == userId)
    //         .FirstOrDefaultAsync(predicate);
    // }
}