using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Viseo.Application.Common.Interfaces;
using Viseo.Application.Request.AuthRequest.GetAuth;
using Viseo.Application.AuthRequest.Queries.RefreshTokenAuth;

namespace Viseo.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IMediator mediator, IJwtAuthManager jwtAuthManager, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _jwtAuthManager = jwtAuthManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAuthQuery command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);

        }

        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<ActionResult> RefreshToken([FromQuery] RefreshTokenAuthQuery command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}