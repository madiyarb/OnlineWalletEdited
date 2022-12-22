using CommonRepository;
using Identity.Application.Contracts;
using Identity.Domain.Entities;
using identity.Infrastructure.Persistance;

namespace identity.Infrastructure.Repositories;

public class UserRepository : BaseRepository<UserDbModel, IdentityDbContext>, IUserRepository
{
    public UserRepository(IdentityDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<UserDbModel> FilterByString(IQueryable<UserDbModel> query, string? filterString)
    {
        return string.IsNullOrEmpty(filterString)
            ? query
            : query.Where(v => v.FirstName.ToLower().Contains(filterString.ToLower())
                               || v.PhoneNumber.ToLower().Contains(filterString.ToLower())
                               || v.Email.ToLower().Contains(filterString.ToLower())
            );
    }
}