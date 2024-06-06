using Domain.Entities.Forms;

namespace Domain.Interfaces
{
    public interface IFormRepository
    {
        // Create section
        Task<int> CreateFormAsync(Form form);
        Task<int> CreateFormDetailAsync(FormDetail formDetail);
        Task<int> CreateFormSignatureMemberAsync(FormSignatureMember formSignatureMember);
        Task<int> CreateFormWorkerList(FormWorker formWorker);
        Task<int> CreateFormPaymentInfo(FormPayment formPaymentInfo);
        Task<int> CreateFormDepartment(FormDepartment formDepartment);
        Task<int> CreateOrderFormAsync(int fromId);
        Task<int> CreateReceiveFormAsync(int fromId);
        Task<int> CreatePayableFormAsync(int fromId);
        
        // Read section
        Task<List<Form>> GetAllFormsAsync();
        Task<Form> GetFormByIdAsync(int formId);
        Task<List<FormDetail>> GetFormDetailsByFormIdAsync(int formId);
        Task<List<FormSignatureMember>> GetFormSignatureMembersByFormIdAsync(int formId);
        Task<List<FormWorker>> GetFormWorkerListByFormIdAsync(int formId);
        Task<List<FormPayment>> GetFormPaymentInfoByFormIdAsync(int formId);
        Task<List<FormDepartment>> GetFormDepartmentsByFormIdAsync(int formId);
        Task<FormSignatureMember> GetFormSignatureMemberAsync(int formId, int userId);
        Task<bool> GetAllSignaturesCheckedAsync(int formId, string stage);

        // Update section
        Task UpdateSignatureAsync(FormSignatureMember formSignatureMemeber);
        Task UpdateDetailAsync(FormDetail formDetail);

        // Delete section
        Task DeleteFormAsync(int formId);
        
    }
}
