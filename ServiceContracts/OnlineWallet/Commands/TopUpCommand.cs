using Ardalis.Result;
using MediatR;
using OnlineWallet.Domain.Entities;
using ServiceContracts.OnlineWallet.Models;

namespace ServiceContracts.OnlineWallet.Commands;

public class TopUpCommand : IRequest<Result<OnlineWalletVm>>
{
    public string WalletUnicode { get; set; }
    public decimal Amount { get; set; }
}