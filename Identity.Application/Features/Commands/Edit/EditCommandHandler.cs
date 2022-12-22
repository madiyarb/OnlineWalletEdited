using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CommonStructures;
using FluentValidation;
using Identity.Application.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using ServiceContracts.Identity.Commands;

namespace Identity.Application.Features.Commands.Edit;

public class EditCommandHandler : IRequestHandler<EditCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<EditCommand> _validator;
    private readonly ILogger<EditCommandHandler> _logger;

    public EditCommandHandler(IUserRepository userRepository, IValidator<EditCommand> validator, ILogger<EditCommandHandler> logger)
    {
        _userRepository = userRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result> Handle(EditCommand request, CancellationToken cancellationToken)
    {
        request.PhoneNumber = request.PhoneNumber.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

        var result = await _userRepository.GetFirstOrDefaultAsync(u => u.Email == request.Email);
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        if (result is null)
        {
            _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Not found with {request.Email} email");
            return Result.Error($"{BussinesErrors.NotFound.ToString()}: Not found with {request.Email} email");
        }

        result.FirstName = request.FirstName;
        result.PhoneNumber = request.PhoneNumber;

        await _userRepository.UpdateAsync(result);
        return Result.Success();
    }
}