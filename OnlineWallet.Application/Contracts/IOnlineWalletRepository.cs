using System.Linq.Expressions;
using CommonRepository.Abstractions;
using OnlineWallet.Domain.Entities;

namespace OnlineWallet.Application.Contracts;

public interface IOnlineWalletRepository : IBaseRepository<OnlineWalletDbModel>
{
}