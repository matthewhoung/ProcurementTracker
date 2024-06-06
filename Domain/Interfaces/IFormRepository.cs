using Domain.Entities.Forms;

namespace Domain.Interfaces
{
    public interface IFormRepository
    {
        // Create section
        Task<int> CreateFormAsync(Form form);
        Task<int> CreateFormDetailAsync(FormDetail formDetail);
        Task<int> CreateFormSignatureMemberAsync(FormSignatureMember formSignatureMember, string stage);
        Task<int> CreateFormWorkerList(FormWorker formWorker);
        Task<int> CreateFormPaymentInfo(FormPayment formPaymentInfo);
        Task<int> CreateFormDepartment(FormDepartment formDepartment);
        // Read section
        Task<List<Form>> GetAllFormsAsync();
        Task<Form> GetFormByIdAsync(int formId);
        Task<List<FormDetail>> GetFormDetailsByFormIdAsync(int formId);
        Task<List<FormSignatureMember>> GetFormSignatureMembersByFormIdAsync(int formId);
        Task<List<FormWorker>> GetFormWorkerListByFormIdAsync(int formId);
        Task<List<FormPayment>> GetFormPaymentInfoByFormIdAsync(int formId);
        Task<List<FormDepartment>> GetFormDepartmentsByFormIdAsync(int formId);
        Task<string> GetFormStageAsync(int formId);
    }
}
