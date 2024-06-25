using Domain.Interfaces;

namespace Application.Forms.Handlers.Queries
{
    public class GetFormCoverDataHandler
    {
        private readonly IFormRepository _formRepository;

        public GetFormCoverDataHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }


    }
}
