using FluentValidation;
using ServiceContracts.Identity.Commands;

namespace Identity.Application.Features.Commands.Edit;

public class EditCommandValidator : AbstractValidator<EditCommand>
{
    public EditCommandValidator()
    {
        RuleFor(q => q.FirstName)
            .NotEmpty().WithMessage("FirstName is Required")
            .NotNull()
            .MaximumLength(100).WithMessage("FirstName must be less than 100 characters long");
        RuleFor(q => q.PhoneNumber)
            .NotEmpty().WithMessage("PhoneNumber is Required")
            .NotNull().WithMessage("PhoneNumber is Required")
            .MinimumLength(11).WithMessage("PhoneNumber must be longer than 11 characters long")
            .MaximumLength(13).WithMessage("PhoneNumber must be less than 13 characters long")
            .Must(phoneNumber => phoneNumber.All(char.IsDigit)).WithMessage("PhoneNumber must contain only numbers");
    }
}