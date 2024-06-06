﻿using Application.Forms.Commands;
using Domain.Entities.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class CreateFormDetailHandler : IRequestHandler<CreateFormDetailCommand, int>
    {
        private readonly IFormRepository _formRepository;

        public CreateFormDetailHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<int> Handle(CreateFormDetailCommand request, CancellationToken cancellationToken)
        {
            var formDetail = new FormDetail
            {
                FormId = request.FormId,
                Title = request.Name,
                Description = request.Description,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                UnitId = request.UnitId,
                Total = request.Quantity * request.UnitPrice,
                IsChecked = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var formDetailId = await _formRepository.CreateFormDetailAsync(formDetail);
            return formDetailId;
        }
    }
}