using Ardalis.Result;
using MediatR;
using ServiceContracts.OnlineWallet.Models;

namespace ServiceContracts.OnlineWallet.Commands;

public class CreateOnlineWalletCommand  : IRequest<Result<OnlineWalletVm>>
{
    public int UserId { get; set; }
}