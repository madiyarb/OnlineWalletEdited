using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.OnlineWallet.Commands;
using ServiceContracts.OnlineWallet.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OnlineWallet.API.Endpoints;

public class TopUp : EndpointBaseAsync
    .WithRequest<TopUpCommand>
    .WithActionResult<DefaultResponseObject<OnlineWalletVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public TopUp(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/OnlineWallet/TopUp")]
    [SwaggerOperation(
        Summary = "Пополнение электронного кошелька",
        Description = "Необходимо передать в теле запроса юникод кошелька и сумму пополнения",
        Tags = new[] { "OnlineWallet" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<OnlineWalletVm>>> HandleAsync([FromBody]TopUpCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<OnlineWalletVm>>(result));
    }
}