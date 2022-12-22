using FluentValidation;
using ServiceContracts.OnlineWallet.Commands;

namespace OnlineWallet.Application.Features.MoneyTransfer;

public class MoneyTransferCommandValidator : AbstractValidator<MoneyTransferCommand>
{
    public MoneyTransferCommandValidator()
    {
        RuleFor(q => q.WalletUnicode)
            .NotEmpty().WithMessage("WalletUnicode is Required.")
            .NotNull();
        RuleFor(q => q.UserId)
            .NotEmpty().WithMessage("UserId is Required.")
            .NotNull();
        RuleFor(q => q.Amount)
            .NotEmpty().WithMessage("Amount is Required.")
            .NotNull();
    }
}