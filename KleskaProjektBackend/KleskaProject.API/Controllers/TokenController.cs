using KleskaProject.Application.Commands;
using KleskaProject.Domain.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KleskaProject.API.Controllers
{
    [ApiController]
    [Route("token")]
    public class TokenController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("valid")]
        public async Task<IActionResult> ValidateToken([FromBody] TokenDto token)
        {
            var result = await _mediator.Send(new ValidateTokenCommand(token));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode((int)result.Error.StatusCode, new { error = result.Error.Details });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            HttpContext.Request.Cookies.TryGetValue("accessToken", out string accessToken);
            HttpContext.Request.Cookies.TryGetValue("refreshToken", out string refreshToken);

            var tokenDto = new TokenDto(accessToken, refreshToken);
            var result = await _mediator.Send(new RefreshTokenCommand(tokenDto));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode((int)result.Error.StatusCode, new { error = result.Error.Details });
        }
    }
}
