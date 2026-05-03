using DevFreela.Application.Commands.DeleteUser;
using DevFreela.Application.Commands.UpdateUser;
using DevFreela.Application.Commands.User.CreateUser;
using DevFreela.Application.Commands.UserLogin;
using DevFreela.Application.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetUserByIdQuery(id);
            var userViewModel = await _mediator.Send(request);
            return Ok(userViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            var newUserId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), newUserId, command);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateUserCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteUserCommand(id);
            await _mediator.Send(request);
            return NoContent();
        }

        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginCommand command)
        {
            var userLoginviewModel = await _mediator.Send(command);
            if (userLoginviewModel == null) return BadRequest();
            return Ok(userLoginviewModel);
        }
    }
}
