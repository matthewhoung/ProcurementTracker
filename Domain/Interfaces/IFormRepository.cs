using Domain.Entities.Forms;

namespace Domain.Interfaces
{
    public interface IFormRepository
    {
        // Create section
        Task<int> CreateFormAsync(Form form);
        Task<int> CreateFormDetailAsync(FormDetail formDetail);
        Task<int> CreateFormSignatureMemberAsync(FormSignatureMember formSignatureMember);
        // Read section
        Task<List<Form>> GetAllFormsAsync();
        Task<Form> GetFormByIdAsync(int formId);
        Task<List<FormDetail>> GetFormDetailsByFormIdAsync(int formId);
        Task<List<FormSignatureMember>> GetFormSignatureMembersByFormIdAsync(int formId);
    }
}
