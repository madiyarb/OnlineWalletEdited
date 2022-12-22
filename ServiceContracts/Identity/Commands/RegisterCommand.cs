using Ardalis.Result;
using MediatR;
using ServiceContracts.Identity.Models;

namespace ServiceContracts.Identity.Commands;

public class RegisterCommand : IRequest<Result<RegisterVm>>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
}