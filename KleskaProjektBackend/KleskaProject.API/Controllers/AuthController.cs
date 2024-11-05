using KleskaProject.Application.Commands;
using KleskaProject.Domain.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KleskaProject.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : Controller
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto request)
    {
        var result = await _mediator.Send(new RegisterUserCommand(request));
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return StatusCode((int)result.Error.StatusCode, new { error = result.Error.Details });
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        var result = await _mediator.Send(new LoginUserCommand(email, password));
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return StatusCode((int)result.Error.StatusCode, new { error = result.Error.Details });
    }
    [HttpGet("valid")]
    public async Task<IActionResult> ValidateToken(String token)
    {
        var result = await _mediator.Send(new ValidateTokenCommand(token));
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return StatusCode((int)result.Error.StatusCode, new { error = result.Error.Details });
    }
}