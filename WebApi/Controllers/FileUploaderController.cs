using Application.Uploaders.Commands;
using Application.Uploaders.Queries;
using Domain.Entities.Commons.FileUploader;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploaderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileUploaderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{formId}")]
        public async Task<ActionResult<int>> UploadFile(int formId, int uploaderId, IFormFile file)
        {
            var command = new CreateFileUploaderCommand
            {
                FormId = formId,
                UploaderId = uploaderId,
                File = file
            };
            var fileId = await _mediator.Send(command);
            return Ok(fileId);
        }

        [HttpGet("{formId}")]
        public async Task<ActionResult<List<FileUploader>>> GetFilePaths(int formId)
        {
            var query = new GetFileUploaderQuery(formId);
            var filePaths = await _mediator.Send(query);
            return Ok(filePaths);
        }
    }
}
