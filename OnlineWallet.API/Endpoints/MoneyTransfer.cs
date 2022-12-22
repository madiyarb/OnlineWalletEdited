using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.OnlineWallet.Commands;
using ServiceContracts.OnlineWallet.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OnlineWallet.API.Endpoints;

public class MoneyTransfer : EndpointBaseAsync
    .WithRequest<MoneyTransferCommand>
    .WithActionResult<DefaultResponseObject<string>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public MoneyTransfer(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/OnlineWallet/MoneyTransfer")]
    [SwaggerOperation(
        Summary = "Перевод денег",
        Description = "Необходимо передать в теле запроса id юзера, юникод кошелька к которому переводятся деньги и сумму перевода",
        Tags = new[] { "OnlineWallet" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<string>>> HandleAsync([FromBody]MoneyTransferCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<OnlineWalletVm>>(result));
    }
}