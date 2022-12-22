using Ardalis.Result;
using AutoMapper;
using CommonStructures;
using Identity.Application.Contracts;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using ServiceContracts.Identity.Models;
using ServiceContracts.Identity.Queries;

namespace Identity.Application.Features.Queries.GetUserData;

public class GetUserDataQueryHandler : IRequestHandler<GetUserDataQuery, Result<UserVm>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetUserDataQueryHandler> _logger;

    public GetUserDataQueryHandler(IUserRepository userRepository, IMapper mapper, ILogger<GetUserDataQueryHandler> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<UserVm>> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
    {
        UserDbModel? user = await _userRepository.GetFirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null)
        {
            _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Not found with {request.Email} email");
            return Result.Error($"{BussinesErrors.NotFound.ToString()}: Not found with {request.Email} email");
        }
        UserVm vm = _mapper.Map<UserVm>(user);
        return Result.Success(vm);
    }
}