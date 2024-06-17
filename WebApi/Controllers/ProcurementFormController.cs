using Application.DTOs;
using Application.Forms.Handlers.Commands;
using Application.Forms.Handlers.Queries;
using Domain.Entities.Forms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("procurement/form")]
    [ApiController]
    public class ProcurementFormController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProcurementFormController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /*
         * Create section
         */

        [HttpPost("create/form")]
        public async Task<IActionResult> CreateForm([FromBody] CreateFormCommand command)
        {
            var formId = await _mediator.Send(command);
            return Ok($@"FormID:{formId} created successfully!");
        }

        [HttpPost("create/signature/member")]
        public async Task<IActionResult> CreateFormSignatureMember([FromBody] CreateFormSignatureMemberCommand command)
        {
            var formSignatureMemberId = await _mediator.Send(command);
            return Ok(formSignatureMemberId);
        }

        [HttpPost("create/payment")]
        public async Task<IActionResult> CreateFormPayment([FromBody] CreateFormPaymentCommand command)
        {
            var formPaymentId = await _mediator.Send(command);
            return Ok(formPaymentId);
        }

        [HttpPost("create/affiliate")]
        public async Task<IActionResult> CreateAffiliateForm([FromBody] CreateAffiliateFormCommand command)
        {
            var affiliateId = await _mediator.Send(command);
            return Ok(affiliateId);
        }

        /*
         * Read section
         */

        [HttpGet("get/allforms")]
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

        [HttpGet("get/{userId}/forms")]
        public async Task<IActionResult> GetUserForms(int userId)
        {
            var query = new GetUserFormsQuery(userId);
            var formInfos = await _mediator.Send(query);
            return Ok(formInfos);
        }

        [HttpGet("get/{formid}/stage")]
        public async Task<IActionResult> GetFormStage(int formid)
        {
            var query = new GetFormStageQuery(formid);
            var formStage = await _mediator.Send(query);
            return Ok(formStage);
        }

        [HttpGet("get/{formid}/details")]
        public async Task<IActionResult> GetFormDetailsByFormId(int formid)
        {
            var query = new GetFormDetailsByFormIdQuery(formid);
            var formDetails = await _mediator.Send(query);
            return Ok(formDetails);
        }

        [HttpGet("get/{formid}/worker")]
        public async Task<IActionResult> GetFormWorkerListByFormId(int formid)
        {
            var query = new GetFormWorkerByFormIdQuery(formid);
            var formWorkers = await _mediator.Send(query);
            return Ok(formWorkers);
        }

        [HttpGet("get/{formid}/payment")]
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

        [HttpGet("get/{formid}/signature/members")]
        public async Task<IActionResult> GetFormSignatureMembersByFormId(int formid)
        {
            var query = new GetFormSignatureMemberByFormIdQuery(formid);
            var formSignatureMembers = await _mediator.Send(query);
            return Ok(formSignatureMembers);
        }

        [HttpGet("get/{formid}/unsigned/members")]
        public async Task<IActionResult> GetUnSignedMember(int formid)
        {
            var query = new GetUnSignedMembersQuery(formid);
            var formSignatureMember = await _mediator.Send(query);
            return Ok(formSignatureMember);
        }

        [HttpGet("get/{formid}/status")]
        public async Task<IActionResult> GetFormStatus(int formid)
        {
            var query = new GetFormStatusQuery(formid);
            var formStatus = await _mediator.Send(query);
            return Ok(formStatus);
        }

        [HttpGet("get/status/counts")]
        public async Task<IActionResult> GetFormStatusCounts()
        {
            var query = new GetFormStatusCountsQuery();
            var formStatusCounts = await _mediator.Send(query);
            return Ok(formStatusCounts);
        }

        /*
         * Update section
         */

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
            return Ok($@"detail with {formDetail.DetailId} updated successfully.");
        }
        [HttpPut("update/payment")]
        public async Task<IActionResult> UpdatePaymentCalculations([FromBody] UpdatePaymentCalculationsDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid payment data.");
            }

            var command = new UpdatePaymentCalculationsCommand(dto);
            await _mediator.Send(command);

            return Ok("Payment calculations updated successfully.");
        }
        [HttpPut("update/detail/ischecked")]
        public async Task<IActionResult> UpdateDetailisChecked(int formId, int detailId)
        {
            var command = new UpdateDetialisCheckedCommand(formId, detailId);
            await _mediator.Send(command);
            return Ok();
        }

        /*
         * Delete section
         */

        [HttpDelete("delete/{formid}")]
        public async Task<IActionResult> DeleteForm(int formid)
        {
            var command = new DeleteFormCommand(formid);
            await _mediator.Send(command);
            return Ok();
        }
    }
}
