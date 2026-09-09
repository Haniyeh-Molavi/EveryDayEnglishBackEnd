using LanguageLearning.Application.Auth.Login;
using LanguageLearning.Application.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LanguageLearning.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}