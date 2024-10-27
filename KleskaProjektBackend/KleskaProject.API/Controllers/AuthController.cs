using KleskaProject.Application.Commands;
using KleskaProject.Domain.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

    [HttpPost("register")]
    public async Task<HttpResponseMessage> Register(UserDto request)
    {
        var result = await _mediator.Send(new RegisterUserCommand(request));
        var httpResult = new HttpResponseMessage();
        if (result.IsSuccess)
        {
            httpResult.StatusCode = HttpStatusCode.OK;
            httpResult.Content = new StringContent("ID: " + result.Value);
            return httpResult;
        }
        httpResult = new HttpResponseMessage(result.Error.StatusCode);
        httpResult.Content = new StringContent("Error: " + result.Error.Details);
        return httpResult;
    }
    [HttpPost("login")]
    public async Task<HttpResponseMessage> Login(string email, string password)
    {
        var result = await _mediator.Send(new LoginUserCommand(email, password));
        var httpResult = new HttpResponseMessage();
        if (result.IsSuccess)
        {
            httpResult.StatusCode = HttpStatusCode.OK;
            httpResult.Content = new StringContent("ID: " + result.Value);
            return httpResult;
        }
        httpResult = new HttpResponseMessage(result.Error.StatusCode);
        httpResult.Content = new StringContent("Error: " + result.Error.Details);
        return httpResult;


    }
    [HttpGet("valid")]
    public async Task<HttpResponseMessage> ValidateToken(String token)
    {
        var result = await _mediator.Send(new ValidateTokenCommand(token));
        var httpResult = new HttpResponseMessage();
        if (result.IsSuccess)
        {
            httpResult.StatusCode = HttpStatusCode.OK;
            httpResult.Content = new StringContent("Token: " + result.Value);
            return httpResult;
        }
        httpResult = new HttpResponseMessage(result.Error.StatusCode);
        httpResult.Content = new StringContent("Error: " + result.Error.Details);
        return httpResult;
    }
}
