using Domain.Entities.Forms;

namespace Domain.Interfaces
{
    public interface IFormRepository
    {
        // Create section
        Task<int> CreateFormAsync(Form form);
        // Read section
        Task<List<Form>> GetAllFormsAsync();
    }
}
