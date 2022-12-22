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

namespace OnlineWallet.Application.Features.TopUp;

public class TopUpCommandHandler : IRequestHandler<TopUpCommand, Result<OnlineWalletVm>>
{
    private readonly IOnlineWalletRepository _onlineWalletRepository;
    private readonly IValidator<TopUpCommand> _validator;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCommandHandler> _logger;

    public TopUpCommandHandler(IOnlineWalletRepository onlineWalletRepository, IValidator<TopUpCommand> validator, IMapper mapper, ILogger<CreateCommandHandler> logger)
    {
        _onlineWalletRepository = onlineWalletRepository;
        _validator = validator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<OnlineWalletVm>> Handle(TopUpCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return Result.Invalid(validationResult.AsErrors());
        
        OnlineWalletDbModel? onlineWallet = await _onlineWalletRepository.GetFirstOrDefaultAsync(u => u.WalletUnicode == request.WalletUnicode);
        if (onlineWallet == null)
        {
            _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Not found with {request.WalletUnicode} WalletUnicode");
            return Result.Error($"{BussinesErrors.NotFound.ToString()}: Not found with {request.WalletUnicode} WalletUnicode");
        }
        onlineWallet.Amount += request.Amount;
        
        try
        {
            await _onlineWalletRepository.UpdateAsync(onlineWallet);
            return Result.Success(_mapper.Map<OnlineWalletVm>(onlineWallet));
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
        }
    }
}