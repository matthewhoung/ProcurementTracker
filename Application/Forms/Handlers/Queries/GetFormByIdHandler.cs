using Application.DTOs;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetFormByIdQuery : IRequest<FormInfoDto>
    {
        public int FormId { get; set; }

        public GetFormByIdQuery(int formId)
        {
            FormId = formId;
        }
    }
    public class GetFormByIdHandler : IRequestHandler<GetFormByIdQuery, FormInfoDto>
    {
        private readonly IFormRepository _formRepository;
        private readonly IFileUploaderRepository _fileUploaderRepository;

        public GetFormByIdHandler(IFormRepository formRepository, IFileUploaderRepository fileUploaderRepository)
        {
            _formRepository = formRepository;
            _fileUploaderRepository = fileUploaderRepository;
        }

        public async Task<FormInfoDto> Handle(GetFormByIdQuery request, CancellationToken cancellationToken)
        {
            var form = await _formRepository.GetFormByIdAsync(request.FormId);
            if (form == null) return null;

            var details = await _formRepository.GetFormDetailsByFormIdAsync(request.FormId);
            var payments = await _formRepository.GetFormPaymentInfoByFormIdAsync(request.FormId);
            var workers = await _formRepository.GetFormWorkerListByFormIdAsync(request.FormId);
            var departments = await _formRepository.GetFormDepartmentsByFormIdAsync(request.FormId);
            var signatures = await _formRepository.GetFormSignatureMembersByFormIdAsync(request.FormId);
            var filePaths = await _fileUploaderRepository.GetFilePathAsync(request.FormId);

            var formInfoDto = new FormInfoDto
            {
                Id = form.Id,
                ProjectId = form.ProjectId,
                Title = form.Title,
                Description = form.Description,
                Stage = form.Stage,
                CreatedAt = form.CreatedAt,
                UpdatedAt = form.UpdatedAt,
                Details = details,
                Signatures = signatures,
                Workers = workers,
                Payments = payments,
                Departments = departments,
                FilePaths = filePaths
            };

            return formInfoDto;
        }
    }
}
