using CommonRepository.Models;
using Identity.Domain.Entities;

namespace OnlineWallet.Domain.Entities;

public class OnlineWalletDbModel : BaseRepositoryEntity
{
    public int UserId { get; set; }
    public string WalletUnicode { get; set; }
    public decimal Amount { get; set; }
}