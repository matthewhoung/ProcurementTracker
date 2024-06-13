﻿using Application.DTOs;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetUserFormsQuery : IRequest<IEnumerable<FormInfoDto>>
    {
        public int UserId { get; set; }

        public GetUserFormsQuery(int userId)
        {
            UserId = userId;
        }
    }
    public class GetUserFormsQueryHandler : IRequestHandler<GetUserFormsQuery, IEnumerable<FormInfoDto>>
    {
        private readonly IFormRepository _formRepository;
        private readonly IFileUploaderRepository _fileUploaderRepository;

        public GetUserFormsQueryHandler(IFormRepository formRepository, IFileUploaderRepository fileUploaderRepository)
        {
            _formRepository = formRepository;
            _fileUploaderRepository = fileUploaderRepository;
        }

        public async Task<IEnumerable<FormInfoDto>> Handle(GetUserFormsQuery request, CancellationToken cancellationToken)
        {
            var formIds = await _formRepository.GetUserFormIdsAsync(request.UserId);
            var formInfoDtos = new List<FormInfoDto>();

            foreach (var formId in formIds)
            {
                var form = await _formRepository.GetFormByIdAsync(formId);
                if (form == null) continue;

                var details = await _formRepository.GetFormDetailsByFormIdAsync(formId);
                var payments = await _formRepository.GetFormPaymentInfoByFormIdAsync(formId);
                var workers = await _formRepository.GetFormWorkerListByFormIdAsync(formId);
                var departments = await _formRepository.GetFormDepartmentsByFormIdAsync(formId);
                var signatures = await _formRepository.GetFormSignatureMembersByFormIdAsync(formId);
                var filePaths = await _fileUploaderRepository.GetFilePathAsync(formId);
                var status = await _formRepository.GetFormStatusAsync(formId);
                var affiliateForms = await _formRepository.GetAllAffiliateFormsAsync(formId);

                var formInfoDto = new FormInfoDto
                {
                    Id = form.Id,
                    ProjectId = form.ProjectId,
                    Title = form.Title,
                    Description = form.Description,
                    Stage = form.Stage,
                    Status = status,
                    CreatedAt = form.CreatedAt,
                    UpdatedAt = form.UpdatedAt,
                    Details = details,
                    Signatures = signatures,
                    Workers = workers,
                    Payments = payments,
                    Departments = departments,
                    FilePaths = filePaths,
                    Affiliates = affiliateForms
                };

                formInfoDtos.Add(formInfoDto);
            }

            return formInfoDtos;
        }
    }
}
