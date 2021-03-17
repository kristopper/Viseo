using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Viseo.Application.Request.UserRequest.Command.CreateUser;
using Viseo.Application.Request.UserRequest.Command.DeleteUser;
using Viseo.Application.Request.UserRequest.Command.UpdateUser;
using Viseo.Application.Request.UserRequest.Queries.GetAllUser;
using Viseo.Application.Request.UserRequest.Queries.GetAllUserByType;

namespace Viseo.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllUserQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllUserByTypeQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateInfo(CreateUserRequestCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser(DeleteUserRequestCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateUserRequestCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}