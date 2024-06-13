using Application.Generic.Handlers.Commands;
using Application.Generic.Handlers.Queries;
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

        /*
         * Create section
         */
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

        /*
         * Read section
         */

        [HttpGet("mutual/worker/class")]
        public async Task<IActionResult> GetAllWorkerClasses()
        {
            var query = new GetAllWorkerClassesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("mutual/worker/type")]
        public async Task<IActionResult> GetAllWorkerTypes()
        {
            var query = new GetAllWorkerTypesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("mutual/worker/team")]
        public async Task<IActionResult> GetAllWorkerTeams()
        {
            var query = new GetAllWorkerTeamsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("mutual/payby")]
        public async Task<IActionResult> GetAllPayBys()
        {
            var query = new GetAllPayByQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("mutual/paytype")]
        public async Task<IActionResult> GetAllPayTypes()
        {
            var query = new GetAllPayTypesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("mutual/departments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var query = new GetAllDepartmentsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("mutual/roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var query = new GetAllRolesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("mutual/unit")]
        public async Task<IActionResult> GetAllUnits()
        {
            var query = new GetAllUnitsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
