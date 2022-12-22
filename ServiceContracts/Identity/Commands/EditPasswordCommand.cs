using Ardalis.Result;
using MediatR;

namespace ServiceContracts.Identity.Commands;

public class EditPasswordCommand : IRequest<Result>
{
    public string Email { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}