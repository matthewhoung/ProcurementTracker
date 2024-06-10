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
        Task<int> CreateOrderFormAsync(int formId);
        Task<int> CreateReceiveFormAsync(int formId);
        Task<int> CreatePayableFormAsync(int formId);
        Task<int> CreateAffiliateFormAsync(int formId, int affiliateFormId);
        
        // Read section
        Task<List<Form>> GetAllFormsAsync();
        Task<Form> GetFormByIdAsync(int formId);
        Task<string> GetFormStageAsync(int formId);
        Task<string> GetFormStatusAsync(int formId);
        Task<List<FormDetail>> GetFormDetailsByFormIdAsync(int formId);
        Task<List<FormWorker>> GetFormWorkerListByFormIdAsync(int formId);
        Task<List<FormPayment>> GetFormPaymentInfoByFormIdAsync(int formId);
        Task<List<FormDepartment>> GetFormDepartmentsByFormIdAsync(int formId);
        Task<List<FormSignatureMember>> GetFormSignatureMembersByFormIdAsync(int formId);
        Task<FormSignatureMember> GetUnSignedMemberAsync(int formId);
        Task<bool> GetAllSignaturesCheckedAsync(int formId);

        // Update section
        Task UpdateSignatureAsync(FormSignatureMember formSignatureMemeber);
        Task UpdateDetailAsync(FormDetail formDetail);
        Task UpdateFormStageAsync(int formId, string stage);
        Task UpdateFormStatusAsync(int formId, string stage, string status);

        // Delete section
        Task DeleteFormAsync(int formId);

        // New Section
        Task UpdateFormDetailisCheckAsync(int formId, int detailId);
        Task UpdatePaymentAmountAsync(int formId);
        Task UpdatePaymentAsync(FormPayment formPayment);
        Task<List<int>> GetAffiliateFormIdsAsync(int formId);
        Task<int> GetFormDetailSumTotalAsync(int formId);
        Task<int> GetPaymentAmountAsync(int formId);
    }
}
