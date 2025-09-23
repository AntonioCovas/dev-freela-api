using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string query)
        {
            var projects = await _mediator.Send(new GetAllProjectsQuery(query));
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0) return BadRequest();
            var request = new GetProjectByIdQuery(id);
            var projectViewModel = await _mediator.Send(request);
            if(projectViewModel == null) return NotFound();
            return Ok(projectViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            if(command.Title?.Length > 50) return BadRequest();
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
        {
            if (command.Description?.Length > 200 || id == 0) return BadRequest();
            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromBody] DeleteProjectCommand command)
        {
            if (id == 0) return BadRequest();
            if (command == null || command.Id == 0) command = new DeleteProjectCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment([FromBody] CreateCommentCommand command)
        {
            if (command.ProjectId == 0 || command.UserId == 0) return BadRequest();
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            if (id == 0) return BadRequest();
            var command = new CancelProjectCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}/start")]
        public async Task<IActionResult> Start(int id)
        {
            if (id == 0) return BadRequest();
            var command = new StartProjectCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}/finish")]
        public async Task<IActionResult> Finish(int id)
        {
            if (id == 0) return BadRequest();
            var command = new FinishProjectCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
