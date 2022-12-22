using FluentValidation;
using OnlineWallet.Application.Contracts;
using ServiceContracts.OnlineWallet.Commands;

namespace OnlineWallet.Application.Features.Create;

public class CreateCommandValidator : AbstractValidator<CreateOnlineWalletCommand>
{
    public CreateCommandValidator(IOnlineWalletRepository onlineWalletRepository)
    {
        RuleFor(q => q.UserId)
            .NotEmpty().WithMessage("UserId is Required.")
            .NotNull();
    }
}