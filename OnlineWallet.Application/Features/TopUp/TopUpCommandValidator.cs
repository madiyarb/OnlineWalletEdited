using FluentValidation;
using ServiceContracts.OnlineWallet.Commands;

namespace OnlineWallet.Application.Features.TopUp;

public class TopUpCommandValidator : AbstractValidator<TopUpCommand>
{
    public TopUpCommandValidator()
    {
        RuleFor(q => q.WalletUnicode)
            .NotEmpty().WithMessage("WalletUnicode is Required.")
            .NotNull();
        RuleFor(q => q.Amount)
            .NotEmpty().WithMessage("Amount is Required.")
            .NotNull();
    }
}