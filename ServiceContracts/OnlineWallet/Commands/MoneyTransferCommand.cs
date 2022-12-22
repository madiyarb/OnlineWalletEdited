using Ardalis.Result;
using MediatR;

namespace ServiceContracts.OnlineWallet.Commands;

public class MoneyTransferCommand : IRequest<Result<string>>
{
    public string WalletUnicode { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
}