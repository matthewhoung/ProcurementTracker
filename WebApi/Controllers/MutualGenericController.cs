using Application.Generic.Handlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("Department")]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentCommand command)
        {
            var departmentId = await _mediator.Send(command);
            return Ok(departmentId);
        }
    }
}
