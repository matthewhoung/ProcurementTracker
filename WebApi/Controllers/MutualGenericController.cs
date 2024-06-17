using Application.Generic.Handlers.Commands;
using Application.Generic.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("mutual/data")]
    [ApiController]
    public class MutualGenericController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MutualGenericController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /*
         * Create section
         */
        [HttpPost("worker/class")]
        public async Task<IActionResult> CreateWorkerClass([FromBody] CreateWorkerClassCommand command)
        {
            await _mediator.Send(command);
            return Ok(command);
        }

        [HttpPost("worker/type")]
        public async Task<IActionResult> CreateWorkerType([FromBody] CreateWorkerTypeCommand command)
        {
            await _mediator.Send(command);
            return Ok(command);
        }

        [HttpPost("worker/team")]
        public async Task<IActionResult> CreateWorkerTeam([FromBody] CreateWorkerTeamCommand command)
        {
            await _mediator.Send(command);
            return Ok(command);
        }

        [HttpPost("payby")]
        public async Task<IActionResult> CreatePayBy([FromBody] CreatePayByCommand command)
        {
            await _mediator.Send(command);
            return Ok(command);
        }

        [HttpPost("paytype")]
        public async Task<IActionResult> CreatePayType([FromBody] CreatePayTypeCommand command)
        {
            await _mediator.Send(command);
            return Ok(command);
        }

        [HttpPost("roles")]
        public async Task<IActionResult> CreateRoles([FromBody] CreateRolesCommand command)
        {
            await _mediator.Send(command);
            return Ok(command);
        }

        [HttpPost("unit")]
        public async Task<IActionResult> CreateUnit([FromBody] CreateUnitClassCommand command)
        {
            await _mediator.Send(command);
            return Ok(command);
        }

        [HttpPost("department")]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentCommand command)
        {
            await _mediator.Send(command);
            return Ok(command);
        }

        /*
         * Read section
         */

        [HttpGet("worker/class")]
        public async Task<IActionResult> GetAllWorkerClasses()
        {
            var query = new GetAllWorkerClassesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("worker/type")]
        public async Task<IActionResult> GetAllWorkerTypes()
        {
            var query = new GetAllWorkerTypesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("worker/team")]
        public async Task<IActionResult> GetAllWorkerTeams()
        {
            var query = new GetAllWorkerTeamsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("payby")]
        public async Task<IActionResult> GetAllPayBys()
        {
            var query = new GetAllPayByQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("paytype")]
        public async Task<IActionResult> GetAllPayTypes()
        {
            var query = new GetAllPayTypesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("departments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var query = new GetAllDepartmentsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var query = new GetAllRolesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("unit")]
        public async Task<IActionResult> GetAllUnits()
        {
            var query = new GetAllUnitsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
