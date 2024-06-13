using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using MediatR;

namespace Application.Generic.Handlers.Commands
{
    public class CreateDepartmentCommand : IRequest<int>
    {
        public Department Department { get; set; }
    }
    public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, int>
    {
        private readonly IGenericRepository _genericRepository;

        public CreateDepartmentHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            await _genericRepository.CreateDepartmentAsync(request.Department);
            return request.Department.DepartmentId;
        }
    }
}
