using Ardalis.Result;
using MediatR;
using ServiceContracts.OnlineWallet.Models;

namespace ServiceContracts.OnlineWallet.Queries;

public class GetByUserIdQuery  : IRequest<Result<OnlineWalletVm>>
{
    public int UserId { get; set; }
}