using ServiceContracts.OnlineWallet.Models;

namespace ServiceContracts.Identity.Models;

public class UserVm
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public OnlineWalletVm OnlineWallet { get; set; }
}