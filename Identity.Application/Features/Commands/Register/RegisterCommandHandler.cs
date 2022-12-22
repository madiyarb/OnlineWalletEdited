using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using CommonStructures;
using FluentValidation;
using Identity.Application.Contracts;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using ServiceContracts.Identity.Commands;
using ServiceContracts.Identity.Models;
using StringHash;

namespace Identity.Application.Features.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterVm>>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<RegisterCommand> _validator;
    private readonly IMapper _mapper;
    private readonly ILogger<RegisterCommandHandler> _logger;

    public RegisterCommandHandler(IUserRepository userRepository,
        IValidator<RegisterCommand> validator,
        IMapper mapper, ILogger<RegisterCommandHandler> logger)
    {
        _userRepository = userRepository;
        _validator = validator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<RegisterVm>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        request.PhoneNumber = request.PhoneNumber.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return Result.Invalid(validationResult.AsErrors());

        request.Password = request.Password.Hash();

        UserDbModel user = _mapper.Map<UserDbModel>(request);

        try
        {
            var result = await _userRepository.AddAsync(user);
            return Result.Success(_mapper.Map<RegisterVm>(result));
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
        }
    }
}