using Ardalis.Result;
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
using ServiceContracts.OnlineWallet.Queries;

namespace OnlineWallet.Application.Features.GetByUserId;

public class GetByUserIdCommandHandler : IRequestHandler<GetByUserIdQuery, Result<OnlineWalletVm>>
{
    private readonly IOnlineWalletRepository _onlineWalletRepository;
    private readonly IValidator<GetByUserIdQuery> _validator;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCommandHandler> _logger;


    public GetByUserIdCommandHandler(IOnlineWalletRepository onlineWalletRepository, IValidator<GetByUserIdQuery> validator, IMapper mapper, ILogger<CreateCommandHandler> logger)
    {
        _onlineWalletRepository = onlineWalletRepository;
        _validator = validator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<OnlineWalletVm>> Handle(GetByUserIdQuery request, CancellationToken cancellationToken)
    {
        OnlineWalletDbModel? onlineWallet = await _onlineWalletRepository.GetFirstOrDefaultAsync(u => u.UserId == request.UserId);
        if (onlineWallet == null)
        {
            _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Not found with {request.UserId} UserId");
            return Result.Error($"{BussinesErrors.NotFound.ToString()}: Not found with {request.UserId} UserId");
        }
        OnlineWalletVm vm = _mapper.Map<OnlineWalletVm>(onlineWallet);
        return Result.Success(vm);
    }
}