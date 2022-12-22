using CommonRepository.Models;

namespace Identity.Domain.Entities;

public class UserDbModel : BaseRepositoryEntity
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }

    // public int OnlineWalletId { get; set; }
    // public OnlineWalletDbModel OnlineWallet { get; set; }
}