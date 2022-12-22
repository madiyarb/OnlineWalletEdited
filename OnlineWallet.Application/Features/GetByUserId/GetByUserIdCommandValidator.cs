using FluentValidation;
using ServiceContracts.OnlineWallet.Commands;
using ServiceContracts.OnlineWallet.Queries;

namespace OnlineWallet.Application.Features.GetByUserId;

public class GetByUserIdCommandValidator : AbstractValidator<GetByUserIdQuery>
{
    public GetByUserIdCommandValidator()
    {
        RuleFor(q => q.UserId)
            .NotEmpty().WithMessage("UserId is Required.")
            .NotNull();
    }
}