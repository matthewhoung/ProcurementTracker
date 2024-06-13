using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using MediatR;

namespace Application.Generic.Handlers.Commands
{
    public class CreateDepartmentCommand : IRequest<string>
    {
        public Department Department { get; set; }
    }
    public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, string>
    {
        private readonly IGenericRepository _genericRepository;

        public CreateDepartmentHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<string> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            await _genericRepository.CreateDepartmentAsync(request.Department);
            return request.Department.DepartmentName;
        }
    }
}
