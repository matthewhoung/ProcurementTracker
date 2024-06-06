using Application.Forms.Handlers.Commands;
using Application.Forms.Handlers.Queries;
using Domain.Entities.Forms;
using MediatR;
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
        [HttpPost("create/orderform")]
        public async Task<IActionResult> CreateOrderForm([FromBody] CreateOrderFormCommand command)
        {
            var formId = await _mediator.Send(command);
            return Ok(formId);
        }

        [HttpPost("create/receiveform")]
        public async Task<IActionResult> CreateReceiveForm([FromBody] CreateReceiveFormCommand command)
        {
            var formId = await _mediator.Send(command);
            return Ok(formId);
        }

        [HttpPost("create/payableform")]
        public async Task<IActionResult> CreatePayableForm([FromBody] CreatePayableFormCommand command)
        {
            var formId = await _mediator.Send(command);
            return Ok(formId);
        }

        [HttpPost("create/detail")]
        public async Task<IActionResult> CreateFormDetail([FromBody] CreateFormDetailCommand command)
        {
            var formDetailId = await _mediator.Send(command);
            return Ok(formDetailId);
        }

        [HttpPost("create/signature/member")]
        public async Task<IActionResult> CreateFormSignatureMember([FromBody] CreateFormSignatureMemberCommand command)
        {
            var formSignatureMemberId = await _mediator.Send(command);
            return Ok(formSignatureMemberId);
        }

        [HttpPost("create/worker")]
        public async Task<IActionResult> CreateFormWorker([FromBody] CreateFormWorkerCommand command)
        {
            var formWorkerId = await _mediator.Send(command);
            return Ok(formWorkerId);
        }

        [HttpPost("create/payment")]
        public async Task<IActionResult> CreateFormPayment([FromBody] CreateFormPaymentCommand command)
        {
            var formPaymentId = await _mediator.Send(command);
            return Ok(formPaymentId);
        }

        [HttpPost("create/department")]
        public async Task<IActionResult> CreateFormDepartment([FromBody] CreateFormDepartmentCommand command)
        {
            var formDepartmentId = await _mediator.Send(command);
            return Ok(formDepartmentId);
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAllForms()
        {
            var query = new GetAllFormsQuery();
            var forms = await _mediator.Send(query);
            return Ok(forms);
        }

        [HttpGet("get/{formid}")]
        public async Task<IActionResult> GetFormById(int formid)
        {
            var query = new GetFormByIdQuery(formid);
            var form = await _mediator.Send(query);
            return Ok(form);
        }

        [HttpGet("get/{formid}/details")]
        public async Task<IActionResult> GetFormDetailsByFormId(int formid)
        {
            var query = new GetFormDetailsByFormIdQuery(formid);
            var formDetails = await _mediator.Send(query);
            return Ok(formDetails);
        }

        [HttpGet("get/{formid}/signature/members")]
        public async Task<IActionResult> GetFormSignatureMembersByFormId(int formid)
        {
            var query = new GetFormSignatureMemberByFormIdQuery(formid);
            var formSignatureMembers = await _mediator.Send(query);
            return Ok(formSignatureMembers);
        }

        [HttpGet("get/{formid}/worker/list")]
        public async Task<IActionResult> GetFormWorkerListByFormId(int formid)
        {
            var query = new GetFormWorkerByFormIdQuery(formid);
            var formWorkers = await _mediator.Send(query);
            return Ok(formWorkers);
        }

        [HttpGet("get/{formid}/payment/info")]
        public async Task<IActionResult> GetFormPaymentInfoByFormId(int formid)
        {
            var query = new GetFormPaymentByFormIdQuery(formid);
            var formPaymentInfo = await _mediator.Send(query);
            return Ok(formPaymentInfo);
        }

        [HttpGet("get/{formid}/departments")]
        public async Task<IActionResult> GetFormDepartmentsByFormId(int formid)
        {
            var query = new GetFormDepartmentByFormIdQuery(formid);
            var formDepartments = await _mediator.Send(query);
            return Ok(formDepartments);
        }

        [HttpPut("update/signature")]
        public async Task<IActionResult> UpdateSignature(int formId, int userId)
        {
            var command = new UpdateSignatureCommand(formId, userId);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("update/detail")]
        public async Task<IActionResult> UpdateDetail([FromBody] FormDetail formDetail)
        {
            var command = new UpdateDetailCommand { FormDetail = formDetail };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("delete/{formid}")]
        public async Task<IActionResult> DeleteForm(int formid)
        {
            var command = new DeleteFormCommand(formid);
            await _mediator.Send(command);
            return Ok();
        }
    }
}
