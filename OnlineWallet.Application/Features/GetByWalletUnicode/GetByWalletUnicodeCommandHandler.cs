using Ardalis.Result;
using AutoMapper;
using CommonStructures;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineWallet.Application.Contracts;
using OnlineWallet.Domain.Entities;
using ServiceContracts.OnlineWallet.Models;
using ServiceContracts.OnlineWallet.Queries;

namespace OnlineWallet.Application.Features.GetByWalletUnicode;

public class GetByWalletUnicodeCommandHandler : IRequestHandler<GetByWalletUnicodeQuery, Result<OnlineWalletVm>>
{
    private readonly IOnlineWalletRepository _onlineWalletRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetByWalletUnicodeCommandHandler> _logger;

    public GetByWalletUnicodeCommandHandler(IOnlineWalletRepository onlineWalletRepository, IMapper mapper, ILogger<GetByWalletUnicodeCommandHandler> logger)
    {
        _onlineWalletRepository = onlineWalletRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<OnlineWalletVm>> Handle(GetByWalletUnicodeQuery request, CancellationToken cancellationToken)
    {
        OnlineWalletDbModel? onlineWallet = await _onlineWalletRepository.GetFirstOrDefaultAsync(u => u.WalletUnicode == request.WalletUnicode);
        if (onlineWallet == null)
        {
            _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Not found with {request.WalletUnicode} WalletUnicode");
            return Result.Error($"{BussinesErrors.NotFound.ToString()}: Not found with {request.WalletUnicode} WalletUnicode");
        }
        OnlineWalletVm vm = _mapper.Map<OnlineWalletVm>(onlineWallet);
        return Result.Success(vm);
    }
}