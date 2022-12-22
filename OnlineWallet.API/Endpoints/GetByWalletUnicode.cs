using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.OnlineWallet.Models;
using ServiceContracts.OnlineWallet.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace OnlineWallet.API.Endpoints;

public class GetByWalletUnicode : EndpointBaseAsync
    .WithRequest<GetByWalletUnicodeQuery>
    .WithActionResult<DefaultResponseObject<OnlineWalletVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetByWalletUnicode(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/OnlineWallet/GetByWalletUnicode")]
    [SwaggerOperation(
        Summary = "Необходимо передать в строке запроса по юникоду кошелька",
        Description = "Необходимо передать в строке запроса по юникоду кошелька",
        Tags = new[] { "OnlineWallet" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<OnlineWalletVm>>> HandleAsync([FromQuery]GetByWalletUnicodeQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<OnlineWalletVm>>(result));
    }
}