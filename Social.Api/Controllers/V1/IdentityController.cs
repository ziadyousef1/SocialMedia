using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social.Api.Contracts.Identity;
using Social.Application.Identity.Commands;

namespace Social.Api.Controllers.V1;

[Route(ApiRoutes.BaseRoute)]
[ApiController]
public class IdentityController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public IdentityController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost(ApiRoutes.Identity.Registration)]
    
    public  async Task<IActionResult> Register([FromBody] UserRegistration registration)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = _mapper.Map<RegisterCommand>(registration);
        var response = await _mediator.Send(command);

        if (!response.IsSuccess)
            return HandleErrorResponse(response.Errors);
        var authResult = new  AuthResult{ Token = response.Payload };
          return Ok(authResult);
    }
    [HttpPost(ApiRoutes.Identity.Login)]
    public async Task<IActionResult> Login([FromBody] Login login)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = _mapper.Map<LoginCommand>(login);
        var response = await _mediator.Send(command);

        if (!response.IsSuccess)
            return HandleErrorResponse(response.Errors);

        return Ok(response.Payload);
    }

}