﻿using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Identity.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.API.Endpoints;

public class EditPassword : EndpointBaseAsync
    .WithRequest<EditPasswordCommand>
    .WithActionResult<DefaultResponseObject<string>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;


    public EditPassword(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    
    [HttpPost("/User/EditPassword")]
    [SwaggerOperation(
        Summary = "Редактирование пароля пользователя",
        Description = "Необходимо передать в теле запроса новые данные пользователя",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<string>>> HandleAsync([FromBody]EditPasswordCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<string>>(result));
    }
}