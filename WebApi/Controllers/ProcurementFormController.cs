using Application.Forms.Commands;
using Application.Forms.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/form")]
    [ApiController]
    public class ProcurementFormController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProcurementFormController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateForm([FromBody] CreateFormCommand command)
        {
            var formId = await _mediator.Send(command);
            return Ok(formId);
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAllForms()
        {
            var query = new GetAllFormsQuery();
            var forms = await _mediator.Send(query);
            return Ok(forms);
        }
    }
}
