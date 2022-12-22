using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.OnlineWallet.Commands;
using ServiceContracts.OnlineWallet.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OnlineWallet.API.Endpoints;

public class Create : EndpointBaseAsync
    .WithRequest<CreateOnlineWalletCommand>
    .WithActionResult<DefaultResponseObject<OnlineWalletVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Create(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/OnlineWallet/Create")]
    [SwaggerOperation(
        Summary = "Создание электронного кошелька",
        Description = "Необходимо передать в теле запроса id юзера",
        Tags = new[] { "OnlineWallet" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<OnlineWalletVm>>> HandleAsync([FromBody]CreateOnlineWalletCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<OnlineWalletVm>>(result));
    }
}