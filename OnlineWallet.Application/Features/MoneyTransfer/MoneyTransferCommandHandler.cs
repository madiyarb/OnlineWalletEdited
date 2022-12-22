using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using CommonStructures;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineWallet.Application.Contracts;
using OnlineWallet.Application.Features.Create;
using OnlineWallet.Domain.Entities;
using ServiceContracts.OnlineWallet.Commands;
using ServiceContracts.OnlineWallet.Models;

namespace OnlineWallet.Application.Features.MoneyTransfer;

public class MoneyTransferCommandHandler : IRequestHandler<MoneyTransferCommand, Result<string>>
{
    private readonly IOnlineWalletRepository _onlineWalletRepository;
    private readonly IValidator<MoneyTransferCommand> _validator;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCommandHandler> _logger;

    public MoneyTransferCommandHandler(IOnlineWalletRepository onlineWalletRepository, IValidator<MoneyTransferCommand> validator, IMapper mapper, ILogger<CreateCommandHandler> logger)
    {
        _onlineWalletRepository = onlineWalletRepository;
        _validator = validator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(MoneyTransferCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return Result.Invalid(validationResult.AsErrors());
        
        OnlineWalletDbModel? onlineWallet = await _onlineWalletRepository.GetFirstOrDefaultAsync(u => u.UserId == request.UserId);

        OnlineWalletDbModel? onlineWalletToTransfer = await _onlineWalletRepository.GetFirstOrDefaultAsync(u => u.WalletUnicode == request.WalletUnicode);
        if (onlineWallet == null)
        {
            _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Not found with {request.UserId} UserId");
            return Result.Error($"{BussinesErrors.NotFound.ToString()}: Not found with {request.UserId} UserId");
        }

        if (onlineWalletToTransfer is null)
        {
            _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Not found with {request.WalletUnicode} WalletUnicode");
            return Result.Error($"{BussinesErrors.NotFound.ToString()}: Not found with {request.WalletUnicode} WalletUnicode");
        }

        onlineWallet.Amount -= request.Amount;
        onlineWalletToTransfer.Amount += request.Amount;
        
        try
        {
            await _onlineWalletRepository.UpdateAsync(onlineWallet);
            await _onlineWalletRepository.UpdateAsync(onlineWalletToTransfer);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
        }
    }
}