using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using CommonStructures;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineWallet.Application.Contracts;
using OnlineWallet.Domain.Entities;
using ServiceContracts.OnlineWallet.Commands;
using ServiceContracts.OnlineWallet.Models;

namespace OnlineWallet.Application.Features.Create;

public class CreateCommandHandler : IRequestHandler<CreateOnlineWalletCommand, Result<OnlineWalletVm>>
{
    private readonly IOnlineWalletRepository _onlineWalletRepository;
    private readonly IValidator<CreateOnlineWalletCommand> _validator;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCommandHandler> _logger;

    public CreateCommandHandler(IOnlineWalletRepository onlineWalletRepository, IValidator<CreateOnlineWalletCommand> validator, IMapper mapper, ILogger<CreateCommandHandler> logger)
    {
        _onlineWalletRepository = onlineWalletRepository;
        _validator = validator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<OnlineWalletVm>> Handle(CreateOnlineWalletCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return Result.Invalid(validationResult.AsErrors());
        
        OnlineWalletDbModel onlineWallet = _mapper.Map<OnlineWalletDbModel>(request);
        onlineWallet.WalletUnicode = Guid.NewGuid().ToString();
        onlineWallet.Amount = 10000;
        try
        {
            var result = await _onlineWalletRepository.AddAsync(onlineWallet);
            return Result.Success(_mapper.Map<OnlineWalletVm>(result));
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
        }
    }
}