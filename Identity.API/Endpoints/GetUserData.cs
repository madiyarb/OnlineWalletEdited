using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Identity.Models;
using ServiceContracts.Identity.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.API.Endpoints;

public class GetUserData : EndpointBaseAsync
    .WithRequest<GetUserDataQuery>
    .WithActionResult<DefaultResponseObject<UserVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetUserData(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/User/GetUserData")]
    [SwaggerOperation(
        Summary = "Получение данных о пользователе",
        Description = "Для получения данных о пользователе необходимо передать его email через параметры в строке",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<UserVm>>> HandleAsync([FromQuery]GetUserDataQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<UserVm>>(result));
    }
}