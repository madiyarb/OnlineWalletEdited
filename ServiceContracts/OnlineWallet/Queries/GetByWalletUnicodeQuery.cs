using Ardalis.Result;
using MediatR;
using ServiceContracts.OnlineWallet.Models;

namespace ServiceContracts.OnlineWallet.Queries;

public class GetByWalletUnicodeQuery  : IRequest<Result<OnlineWalletVm>>
{
    public string WalletUnicode { get; set; }
}