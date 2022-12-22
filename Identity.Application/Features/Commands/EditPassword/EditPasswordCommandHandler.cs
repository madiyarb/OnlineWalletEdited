using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CommonStructures;
using FluentValidation;
using Identity.Application.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using ServiceContracts.Identity.Commands;
using StringHash;

namespace Identity.Application.Features.Commands.EditPassword;

public class EditPasswordCommandHandler : IRequestHandler<EditPasswordCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<EditPasswordCommand> _validator;
    private readonly ILogger<EditPasswordCommandHandler> _logger;

    public EditPasswordCommandHandler(IUserRepository userRepository, IValidator<EditPasswordCommand> validator, ILogger<EditPasswordCommandHandler> logger)
    {
        _userRepository = userRepository;
        _validator = validator;
        _logger = logger;
    }


    public async Task<Result> Handle(EditPasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetFirstOrDefaultAsync(u => u.Email == request.Email);
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        if (result is null)
        {
            _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Not found with {request.Email} email");
            return Result.Error($"{BussinesErrors.NotFound.ToString()}: Not found with {request.Email} email");
        }

        if (!result.Password.VerifyHashedString(request.OldPassword))
            return Result.Error("Неверно введен старый пароль. Повторите попытку");

        if (!request.NewPassword.Equals(request.ConfirmPassword))
            return Result.Error("Пароли не совпадают. Повторите попытку");

        result.Password = request.NewPassword.Hash();

        await _userRepository.UpdateAsync(result);
        return Result.Success();

    }
}