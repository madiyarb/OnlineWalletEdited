using Ardalis.Result;
using MediatR;
using ServiceContracts.Identity.Models;

namespace ServiceContracts.Identity.Queries;

public class GetUserDataQuery : IRequest<Result<UserVm>>
{
    public string Email { get; set; }
}