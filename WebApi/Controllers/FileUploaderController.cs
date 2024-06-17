using Application.Uploaders.DTOs;
using Application.Uploaders.Handlers.Commands;
using Application.Uploaders.Handlers.Queries;
using Domain.Entities.Commons.FileUploader;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("fileuploader")]
    public class FileUploaderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileUploaderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<int>> UploadFile([FromForm] CreateFileUploaderDto dto)
        {
            var command = new CreateFileUploaderCommand
            {
                FileUploaderDto = dto
            };
            var fileId = await _mediator.Send(command);
            return Ok(fileId);
        }

        [HttpGet("download")]
        public async Task<ActionResult<List<FileUploader>>> GetFilePaths(int formId)
        {
            var query = new GetFileUploaderQuery(formId);
            var filePaths = await _mediator.Send(query);
            return Ok(filePaths);
        }
    }
}
