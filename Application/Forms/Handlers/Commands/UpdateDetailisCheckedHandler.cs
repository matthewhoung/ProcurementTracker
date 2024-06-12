using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class  UpdateDetialisCheckedCommand : IRequest<Unit>
    {
        public int FormId { get; set; }
        public int DetailId { get; set; }

        public UpdateDetialisCheckedCommand(int formId, int detailId)
        {
            FormId = formId;
            DetailId = detailId;
        }
    }
    public class UpdateDetailisCheckedHandler : IRequestHandler<UpdateDetialisCheckedCommand, Unit>
    {
        private readonly IFormRepository _formRepository;

        public UpdateDetailisCheckedHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<Unit> Handle(UpdateDetialisCheckedCommand request, CancellationToken cancellationToken)
        {
            await _formRepository.UpdateFormDetailisCheckAsync(request.FormId, request.DetailId);
            return Unit.Value;
        }
    }
}
