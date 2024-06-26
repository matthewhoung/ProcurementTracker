﻿using Domain.Entities.Forms;

namespace Domain.Interfaces
{
    public interface IFormRepository
    {
        /*
         * 
         * Forms新增廢棄狀態as "archived"
         * 
         * 新增細項簽核通過 => 人員才能簽核
         * 
         * Modify UpdatePaymentHandler.cs
         */

        // Create section
        Task<int> CreateFormAsync(Form form);
        Task<int> CreateFormDetailsAsync(IEnumerable<FormDetail> formDetails);
        Task<int> CreateFormSignatureMemberAsync(FormSignatureMember formSignatureMember);
        Task<int> CreateDefaultSignatureMembersAsync(IEnumerable<FormSignatureMember> formSignatureMembers);
        Task<int> CreateFormWorkerAsync(FormWorker formWorker);
        Task<int> CreateFormPaymentInfoAsync(FormPayment formPaymentInfo);
        Task<int> CreateFormDepartmentsAsync(IEnumerable<FormDepartment> formDepartments);
        Task<int> CreateOrderFormAsync(int formId);
        Task<int> CreateReceiveFormAsync(int formId);
        Task<int> CreatePayableFormAsync(int formId);
        Task<int> CreateAffiliateFormAsync(int formId, int affiliateFormId);
        
        // Read section
        Task<List<Form>> GetAllFormsAsync();
        Task<Form> GetFormByIdAsync(int formId);
        Task<List<FormCover>> GetFormCoverDataAsync(int? userId, string stage, string status);
        Task<IEnumerable<int>> GetUserFormIdsAsync(int userId);
        Task<string> GetFormStageAsync(int formId);
        Task<string> GetFormStatusAsync(int formId);
        Task<List<FormStatusCount>> GetFormStatusCountsAsync(string stage);
        Task<List<FormDetail>> GetFormDetailsByFormIdAsync(int formId);
        Task<FormWorker> GetFormWorkerByFormIdAsync(int formId);
        Task<List<FormPayment>> GetFormPaymentInfoByFormIdAsync(int formId);
        Task<List<FormDepartment>> GetFormDepartmentsByFormIdAsync(int formId);
        Task<List<FormSignatureMember>> GetFormSignatureMembersByFormIdAsync(int formId);
        Task<List<FormSignatureMember>> GetFormSignatureByFormIdAndStageAsync(int formId, string stage);
        Task<List<FormSignatureMember>> GetUnSignedMembersAsync(int formId);
        Task<bool> GetAllSignaturesCheckedAsync(int formId);    
        Task<int> GetFormDetailSumTotalAsync(int formId);
        Task<IEnumerable<FormAffiliate>> GetAllAffiliateFormsAsync(int formId);

        // Update section
        Task UpdateSignatureAsync(FormSignatureMember formSignatureMemeber);
        Task UpdateDetailAsync(FormDetail formDetail);
        Task UpdateFormStageAsync(int formId, string stage);
        Task UpdateFormStatusAsync(int formId, string stage, string status);
        Task UpdateFormDetailisCheckAsync(int formId, int detailId);
        Task UpdatePaymentAmountAsync(int formId);
        Task UpdatePaymentAsync(FormPayment formPayment);
        Task UpdateArchiveStatus(int formId,int userId);

        // Delete section
        Task DeleteFormAsync(int formId);
    }
}
