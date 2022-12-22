using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.OnlineWallet.Commands;
using ServiceContracts.OnlineWallet.Models;
using ServiceContracts.OnlineWallet.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace OnlineWallet.API.Endpoints;

public class GetByUserId : EndpointBaseAsync
    .WithRequest<GetByUserIdQuery>
    .WithActionResult<DefaultResponseObject<OnlineWalletVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetByUserId(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpGet("/OnlineWallet/GetByUserId")]
    [SwaggerOperation(
        Summary = "Необходимо передать в строке запроса id юзера",
        Description = "Необходимо передать в строке запроса id юзера",
        Tags = new[] { "OnlineWallet" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<OnlineWalletVm>>> HandleAsync([FromQuery]GetByUserIdQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<OnlineWalletVm>>(result));
    }
}