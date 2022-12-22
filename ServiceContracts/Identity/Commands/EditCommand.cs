using Ardalis.Result;
using MediatR;

namespace ServiceContracts.Identity.Commands;

public class EditCommand : IRequest<Result>
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
}