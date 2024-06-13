using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using MediatR;

namespace Application.Generic.Handlers.Commands
{
    public class CreateRolesCommand : IRequest<int>
    {
        public Roles Roles { get; set; }
    }
    public class CreateRolesHandler : IRequestHandler<CreateRolesCommand, int>
    {
        private readonly IGenericRepository _genericRepository;

        public CreateRolesHandler(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<int> Handle(CreateRolesCommand request, CancellationToken cancellationToken)
        {
            await _genericRepository.CreateRolesAsync(request.Roles);
            return request.Roles.RoleId;
        }
    }
}
