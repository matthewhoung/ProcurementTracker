using Application.Generic.Handlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MutualGenericController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MutualGenericController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("mutual/worker/class")]
        public async Task<IActionResult> CreateWorkerClass([FromBody] CreateWorkerClassCommand command)
        {
            await _mediator.Send(command);
            return Ok(command);
        }

        [HttpPost("mutual/worker/type")]
        public async Task<IActionResult> CreateWorkerType([FromBody] CreateWorkerTypeCommand command)
        {
            await _mediator.Send(command);
            return Ok(command);
        }

        [HttpPost("mutual/worker/team")]
        public async Task<IActionResult> CreateWorkerTeam([FromBody] CreateWorkerTeamCommand command)
        {
            await _mediator.Send(command);
            return Ok(command);
        }

        [HttpPost("mutual/payby")]
        public async Task<IActionResult> CreatePayBy([FromBody] CreatePayByCommand command)
        {
            await _mediator.Send(command);
            return Ok(command);
        }

        [HttpPost("mutual/paytype")]
        public async Task<IActionResult> CreatePayType([FromBody] CreatePayTypeCommand command)
        {
            await _mediator.Send(command);
            return Ok(command);
        }

        [HttpPost("mutual/roles")]
        public async Task<IActionResult> CreateRoles([FromBody] CreateRolesCommand command)
        {
            await _mediator.Send(command);
            return Ok(command);
        }

        [HttpPost("mutual/unit")]
        public async Task<IActionResult> CreateUnit([FromBody] CreateUnitClassCommand command)
        {
            await _mediator.Send(command);
            return Ok(command);
        }

        [HttpPost("mutual/department")]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentCommand command)
        {
            await _mediator.Send(command);
            return Ok(command);
        }
    }
}
