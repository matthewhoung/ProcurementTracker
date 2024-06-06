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
        // Read section
        Task<List<Form>> GetAllFormsAsync();
        Task<Form> GetFormByIdAsync(int formId);
        Task<List<FormDetail>> GetFormDetailsByFormIdAsync(int formId);
        Task<List<FormSignatureMember>> GetFormSignatureMembersByFormIdAsync(int formId);
        Task<List<FormWorker>> GetFormWorkerListByFormIdAsync(int formId);
        Task<List<FormPayment>> GetFormPaymentInfoByFormIdAsync(int formId);
        Task<List<FormDepartment>> GetFormDepartmentsByFormIdAsync(int formId);

        // New methods to be added to handler
        Task<int> CreateOrderFormAsync(int fromId);
        Task<int> CreateReceiveFormAsync(int fromId);
        Task<int> CreatePayableFormAsync(int fromId);
        Task UpdateSignatureAsync(FormSignatureMember formSignatureMemeber);
        Task UpdateDetailAsync(FormDetail formDetail);
        Task DeleteFormAsync(int formId);
    }
}
