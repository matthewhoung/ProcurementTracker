﻿using Dapper;
using Domain.Entities.Forms;
using Domain.Interfaces;
using System.Data;

namespace Infrastructure.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly IDbConnection _dbConnection;

        public FormRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Create section

        public async Task<int> CreateFormAsync(Form form)
        {
            var writeCommand = @"
                INSERT INTO forms
                (
                    project_id,
                    title,
                    description,
                    stage,
                    created_at,
                    updated_at
                )
                VALUES
                (
                    @ProjectId,
                    @Title,
                    @Description,
                    @Stage,
                    @CreatedAt,
                    @UpdatedAt
                );
                SELECT LAST_INSERT_ID();";
            var parameters = new
            {
                form.ProjectId,
                form.Title,
                form.Description,
                form.Stage,
                form.CreatedAt,
                form.UpdatedAt
            };
            var formId = await _dbConnection.ExecuteScalarAsync<int>(writeCommand, parameters);
            await CreateOrderFormAsync(formId);
            return formId;
        }
        public async Task<int> CreateOrderFormAsync(int formId)
        {
            return await CreateSubFormAsync("forms_orderform", formId);
        }
        public async Task<int> CreateReceiveFormAsync(int formId)
        {
            return await CreateSubFormAsync("forms_receiveform", formId);
        }
        public async Task<int> CreatePayableFormAsync(int formId)
        {
            return await CreateSubFormAsync("forms_payableform", formId);
        }
        private async Task<int> CreateSubFormAsync(string tableName, int formId)
        {
            var writeCommand = $@"
                INSERT INTO {tableName}
                (
                    form_id,
                    status
                )
                VALUES
                (
                    @FormId,
                    'pending'
                );
                SELECT LAST_INSERT_ID();";
            var parameters = new
            {
                FormId = formId
            };

            return await _dbConnection.ExecuteScalarAsync<int>(writeCommand, parameters);
        }
        public async Task<int> CreateFormDetailsAsync(IEnumerable<FormDetail> formDetails)
        {
            var writeCommand = @"
                INSERT INTO forms_detail
                (
                    form_id,
                    detail_title,
                    detail_description,
                    quantity,
                    unit_price,
                    unit_id,
                    detail_total,
                    is_checked
                )
                VALUES
                (
                    @FormId,
                    @Title,
                    @Description,
                    @Quantity,
                    @UnitPrice,
                    @UnitId,
                    @Total,
                    @IsChecked
                );";

            var affectedRows = await _dbConnection.ExecuteAsync(writeCommand, formDetails);
            return affectedRows;
        }
        public async Task<int> CreateFormSignatureMemberAsync(FormSignatureMember formSignatureMember)
        {
            var table = formSignatureMember.Stage switch
            {
                "OrderForm" => "order_signatures",
                "ReceiveForm" => "receive_signatures",
                "PayableForm" => "payable_signatures",
                _ => throw new ArgumentException("Invalid stage")
            };
            var writeCommand = $@"
                INSERT INTO {table}
                (
                    form_id,
                    user_id,
                    role_id,
                    is_checked
                )
                VALUES
                (
                    @FormId,
                    @UserId,
                    @RoleId,
                    @IsChecked
                );";
            var parameters = new
            {
                FormId = formSignatureMember.FormId,
                UserId = formSignatureMember.UserId,
                RoleId = formSignatureMember.RoleId,
                IsChecked = formSignatureMember.IsChecked
            };

            return await _dbConnection.ExecuteScalarAsync<int>(writeCommand, parameters);
        }
        public async Task<int> CreateDefaultSignatureMembersAsync(IEnumerable<FormSignatureMember> formSignatureMembers)
        {
            var writeCommand = @"
                INSERT INTO order_signatures
                (
                    form_id,
                    user_id,
                    role_id,
                    is_checked
                )
                VALUES
                (
                    @FormId,
                    @UserId,
                    @RoleId,
                    @IsChecked
                );";

            var affectedRows = await _dbConnection.ExecuteAsync(writeCommand, formSignatureMembers);
            return affectedRows;
        }
        public async Task<int> CreateFormWorkersAsync(IEnumerable<FormWorker> formWorkers)
        {
            var writeCommand = @"
                INSERT INTO forms_worker
                (
                    form_id,
                    worker_type_id,
                    worker_team_id
                )
                VALUES
                (
                    @FormId,
                    @WorkerTypeId,
                    @WorkerTeamId
                );";

            var affectedRows = await _dbConnection.ExecuteAsync(writeCommand, formWorkers);
            return affectedRows;
        }
        public async Task<int> CreateFormPaymentInfoAsync(FormPayment formPaymentInfo)
        {
            var writeCommand = @"
                INSERT INTO forms_payment
                (
                    form_id,
                    payment_total,
                    payment_delta,
                    delta_title_id,
                    payment_amount,
                    payment_title_id,
                    payment_tool_id
                )
                VALUES
                (
                    @FormId,
                    @PaymentTotal,
                    @PaymentDelta,
                    @DeltaTitleId,
                    @PaymentAmount,
                    @PaymentTitleId,
                    @PaymentToolId
                );";
            var payId = await _dbConnection.ExecuteScalarAsync<int>(writeCommand, formPaymentInfo);
            return payId;
        }
        public async Task<int> CreateFormDepartmentsAsync(IEnumerable<FormDepartment> formDepartments)
        {
            var writeCommand = @"
            INSERT INTO forms_department
            (
                form_id,
                department_id
            )
            VALUES
            (
                @FormId,
                @DepartmentId
            );";

            var affectedRows = await _dbConnection.ExecuteAsync(writeCommand, formDepartments);
            return affectedRows;
        }
        public async Task<int> CreateAffiliateFormAsync(int formId, int affiliateFormId)
        {
            var writeCommand = @"
                INSERT INTO forms_affiliateform
                (
                    form_id,
                    affiliate_form_id
                )
                VALUES
                (
                    @FormId,
                    @AffiliateFormId
                );
                SELECT LAST_INSERT_ID();";
            var parameters = new
            {
                FormId = formId,
                AffiliateFormId = affiliateFormId
            };

            var affiliateId = await _dbConnection.ExecuteScalarAsync<int>(writeCommand, parameters);
            return affiliateId;
        }

        // Read section

        public async Task<List<Form>> GetAllFormsAsync()
        {
            var readCommand = @"
                SELECT
                    id AS Id,
                    project_id AS ProjectId,
                    title AS Title,
                    description AS Description,
                    stage AS Stage,
                    created_at AS CreatedAt,
                    updated_at AS UpdatedAt
                FROM
                    forms";
            var forms = await _dbConnection.QueryAsync<Form>(readCommand);
            return forms.AsList();
        }
        public async Task<Form> GetFormByIdAsync(int formId)
        {
            var readCommand = @"
                SELECT
                    id AS Id,
                    project_id AS ProjectId,
                    title AS Title,
                    description AS Description,
                    stage AS Stage,
                    created_at AS CreatedAt,
                    updated_at AS UpdatedAt
                FROM
                    forms
                WHERE
                    id = @FormId";
            var parameters = new { FormId = formId };
            var form = await _dbConnection.QueryFirstOrDefaultAsync<Form>(readCommand, parameters);
            return form;
        }
        public async Task<List<int>> GetAffiliateFormIdsAsync(int formId)
        {
            var readCommand = @"
                SELECT
                    form_affiliate_id
                FROM
                    forms_affiliate
                WHERE
                    form_id = @FormId";
            var parameters = new { FormId = formId };
            var affiliateFormIds = await _dbConnection.QueryAsync<int>(readCommand, parameters);
            return affiliateFormIds.AsList();
        }
        public async Task<string> GetFormStageAsync(int formId)
        {
            var readCommand = @"
                SELECT
                    stage
                FROM
                    forms
                WHERE
                    id = @FormId";
            var parameters = new { FormId = formId };
            var stage = await _dbConnection.QuerySingleOrDefaultAsync<string>(readCommand, parameters);
            
            var table = stage switch
            {
                "OrderForm" => "order_signatures",
                "ReceiveForm" => "receive_signatures",
                "PayableForm" => "payable_signatures",
                _ => throw new ArgumentException("Invalid stage")
            };
            return table;
        }
        public async Task<string> GetFormStatusAsync(int formId)
        {
            var checkList = await GetFormSignatureMembersByFormIdAsync(formId);
            var allChecked = checkList.All(c => c.IsChecked);
            var creatorChecked = checkList.Any(c => c.RoleName == "Creator" && c.IsChecked);
            var status = allChecked ? "finished" : (creatorChecked ? "inprogress" : "pending");
            return status;
        }
        public async Task<List<FormDetail>> GetFormDetailsByFormIdAsync(int formId)
        {
            var readCommand = @"
                SELECT
                    fd.detail_id AS DetailId,
                    fd.form_id AS FormId,
                    fd.detail_title AS Title,
                    fd.detail_description AS Description,
                    fd.quantity AS Quantity,
                    fd.unit_price AS UnitPrice,
                    fd.unit_id AS UnitId,
                    u.unit_name AS UnitName,
                    fd.detail_total AS Total,
                    fd.is_checked AS IsChecked,
                    fd.created_at AS CreatedAt,
                    fd.updated_at AS UpdatedAt
                FROM
                    forms_detail fd
                JOIN
                    units u ON fd.unit_id = u.unit_id
                WHERE
                    fd.form_id = @FormId";
            var parameters = new { FormId = formId };
            var formDetails = await _dbConnection.QueryAsync<FormDetail>(readCommand, parameters);
            return formDetails.AsList();
        }
        public async Task<int> GetFormDetailSumTotalAsync(int formId)
        {
            var readCommand = @"
                SELECT
                    SUM(detail_total)
                FROM
                    forms_detail
                WHERE
                    form_id = @FormId";
            var parameters = new { FormId = formId };
            var total = await _dbConnection.ExecuteScalarAsync<int>(readCommand, parameters);
            return total;
        }
        public async Task<List<FormWorker>> GetFormWorkerListByFormIdAsync(int formId)
        {
            var readCommand = @"
                SELECT
                    fw.worker_id AS WorkerId,
                    f.id AS FormId,
                    fw.worker_type_id AS WorkerTypeId,
                    wt.worker_type_name AS WorkerTypeName,
                    fw.worker_team_id AS WorkerTeamId,
                    wtm.worker_team_name AS WorkerTeamName
                FROM
                    forms f
                JOIN
                    forms_worker fw ON f.id = fw.form_id
                JOIN
                    worker_types wt ON fw.worker_type_id = wt.worker_type_id
                JOIN
                    worker_teams wtm ON wt.worker_type_id = wtm.worker_type_id
                WHERE
                    f.id = @FormId";
            var parameters = new { FormId = formId };
            var formWorkers = await _dbConnection.QueryAsync<FormWorker>(readCommand, parameters);
            return formWorkers.AsList();
        }
        public async Task<List<FormPayment>> GetFormPaymentInfoByFormIdAsync(int formId)
        {
            var readCommand = @"
                SELECT
                    fp.payment_id AS PaymentId,
                    f.id AS FormId,
                    fp.payment_total AS PaymentTotal,
                    fp.payment_delta AS PaymentDelta,
                    fp.delta_title_id AS DeltaTitleId,
                    pt1.pay_type_name AS DeltaTitle,
                    fp.payment_amount AS PaymentAmount,
                    fp.payment_title_id AS PaymentTitleId,
                    pt2.pay_type_name AS PaymentTitle,
                    fp.payment_tool_id AS PaymentToolId,
                    pb.pay_by_name AS PaymentTool
                FROM
                    forms f
                JOIN
                    forms_payment fp ON f.id = fp.form_id
                JOIN
                    pay_types pt1 ON fp.delta_title_id = pt1.pay_type_id
                JOIN
                    pay_types pt2 ON fp.payment_title_id = pt2.pay_type_id
                JOIN
                    pay_by pb ON fp.payment_tool_id = pb.pay_by_id
                WHERE
                    f.id = @FormId";
            var parameters = new { FormId = formId };
            var formPayments = await _dbConnection.QueryAsync<FormPayment>(readCommand, parameters);
            return formPayments.AsList();
        }
        public async Task<List<FormDepartment>> GetFormDepartmentsByFormIdAsync(int formId)
        {
            var readCommand = @"
                SELECT
                    fd.formdepartment_id AS FormDepartmentId,
                    f.id AS FormId,
                    fd.department_id AS DepartmentId,
                    d.department_name AS DepartmentName
                FROM
                    forms f
                JOIN
                    forms_department fd ON f.id = fd.form_id
                JOIN
                    departments d ON fd.department_id = d.department_id
                WHERE
                    f.id = @FormId";
            var parameters = new { FormId = formId };
            var formDepartments = await _dbConnection.QueryAsync<FormDepartment>(readCommand, parameters);
            return formDepartments.AsList();
        }
        public async Task<List<FormSignatureMember>> GetFormSignatureMembersByFormIdAsync(int formId)
        {
            var signatureTable = await GetFormStageAsync(formId);

            var readCommand = $@"
                SELECT
                    fsm.sign_id AS SignId,
                    f.id AS FormId,
                    fsm.user_id AS UserId,
                    r.role_id AS RoleId,
                    r.role_name AS RoleName,
                    f.stage AS Stage,
                    fsm.is_checked AS IsChecked
                FROM
                    forms f
                JOIN
                    {signatureTable} fsm ON f.id = fsm.form_id
                JOIN
                    roles r ON fsm.role_id = r.role_id
                WHERE
                    f.id = @FormId";

            var parameters = new { FormId = formId };
            var formSignatureMembers = await _dbConnection.QueryAsync<FormSignatureMember>(readCommand, parameters);
            return formSignatureMembers.AsList();
        }
        public async Task<FormSignatureMember> GetUnSignedMemberAsync(int formId)
        {
            var signatureTable = await GetFormStageAsync(formId);

            var readCommand = $@"
                SELECT
                    fsm.sign_id AS SignId,
                    fsm.form_id AS FormId,
                    fsm.user_id AS UserId,
                    r.role_id AS RoleId,
                    r.role_name AS RoleName,
                    fsm.is_checked AS IsChecked,
                FROM
                    {signatureTable} fsm
                JOIN
                    roles r ON fsm.role_id = r.role_id
                WHERE
                    fsm.is_checked = 0 AND fms.form_id == @FormId";

            var parametersForRead = new { FormId = formId};
            var formSignatureMember = await _dbConnection.QueryFirstOrDefaultAsync<FormSignatureMember>(readCommand, parametersForRead);
            return formSignatureMember;
        }
        public async Task<bool> GetAllSignaturesCheckedAsync(int formId)
        {
            var table = await GetFormStageAsync(formId);

            var checkCommand = $@"
                SELECT COUNT(status)
                FROM {table}
                WHERE form_id = @FormId AND is_checked = 0";
            var parameters = new { FormId = formId };
            var uncheckedCount = await _dbConnection.ExecuteScalarAsync<int>(checkCommand, parameters);

            return uncheckedCount == 0;
        }
        public async Task<List<FormStatusCount>> GetFormStatusCountsAsync()
        {
            var query = @"
                SELECT 
                    'OrderForm' AS FormType,
                    status,
                    COUNT(*) AS Count
                FROM forms_orderform
                GROUP BY status
                UNION ALL
                SELECT 
                    'ReceiveForm' AS FormType,
                    status,
                    COUNT(*) AS Count
                FROM forms_receiveform
                GROUP BY status
                UNION ALL
                SELECT 
                    'PayableForm' AS FormType,
                    status,
                    COUNT(*) AS Count
                FROM forms_payableform
                GROUP BY status;";

            var formStatusCounts = await _dbConnection.QueryAsync<FormStatusCount>(query);
            return formStatusCounts.ToList();
        }


        // Update section

        public async Task UpdateDetailAsync(FormDetail formDetail)
        {
            var updateCommand = @"
                UPDATE
                    forms_detail
                SET
                    detail_title = @Title,
                    detail_description = @Description,
                    quantity = @Quantity,
                    unit_price = @UnitPrice,
                    unit_id = @UnitId,
                    detail_total = @Total,
                    is_checked = @IsChecked
                WHERE
                    detail_id = @DetailId";
            var parameters = new
            {
                formDetail.DetailId,
                formDetail.Title,
                formDetail.Description,
                formDetail.Quantity,
                formDetail.UnitPrice,
                formDetail.UnitId,
                formDetail.Total,
                formDetail.IsChecked
            };
            await _dbConnection.ExecuteAsync(updateCommand, parameters);
        }
        public async Task UpdateFormDetailisCheckAsync(int formId, int detailId)
        {
            var updateCommand = @"
                UPDATE
                    forms_detail
                SET
                    is_checked = 1
                WHERE
                    form_id = @FormId AND detail_id = @DetailId";
            var parameters = new { FormId = formId, DetailId = detailId };
            await _dbConnection.ExecuteAsync(updateCommand, parameters);
        }
        public async Task UpdateSignatureAsync(FormSignatureMember formSignatureMember)
        {
            var table = formSignatureMember.Stage switch
            {
                "OrderForm" => "order_signatures",
                "ReceiveForm" => "receive_signatures",
                "PayableForm" => "payable_signatures",
                _ => throw new ArgumentException("Invalid stage")
            };

            var updateCommand = $@"
                UPDATE {table}
                SET is_checked = 1
                WHERE form_id = @FormId AND user_id = @UserId";
            var parameters = new { FormId = formSignatureMember.FormId, UserId = formSignatureMember.UserId };
            await _dbConnection.ExecuteAsync(updateCommand, parameters);
        }
        public async Task UpdateFormStageAsync(int formId, string stage)
        {
            var updateCommand = @"
                UPDATE
                    forms
                SET
                    stage = @Stage
                WHERE
                    id = @FormId";
            var parameters = new { FormId = formId, Stage = stage };
            await _dbConnection.ExecuteAsync(updateCommand, parameters);
        }
        public async Task UpdateFormStatusAsync(int formId, string stage, string status)
        {
            string tableName = stage switch
            {
                "OrderForm" => "forms_orderform",
                "ReceiveForm" => "forms_receiveform",
                "PayableForm" => "forms_payableform",
                _ => throw new ArgumentException("Invalid form type")
            };

            var updateCommand = $@"
                UPDATE 
                    {tableName}
                SET 
                    status = @Status
                WHERE 
                    form_id = @FormId";
            var parameters = new { Status = status, FormId = formId };
            await _dbConnection.ExecuteAsync(updateCommand, parameters);
        }
        public async Task UpdatePaymentAsync(FormPayment formPayment)
        {
            var updateCommand = @"
                UPDATE forms_payment
                SET
                    payment_total = @PaymentTotal,
                    payment_delta = @PaymentDelta,
                    delta_title_id = @DeltaTitleId,
                    payment_title_id = @PaymentTitleId,
                    payment_tool_id = @PaymentToolId,
                    payment_amount = @PaymentAmount
                WHERE
                    form_id = @FormId";
            var parameters = new
            {
                formPayment.FormId,
                formPayment.PaymentTotal,
                formPayment.PaymentDelta,
                formPayment.DeltaTitleId,
                formPayment.PaymentTitleId,
                formPayment.PaymentToolId,
                formPayment.PaymentAmount
            };

            await _dbConnection.ExecuteAsync(updateCommand, parameters);
        }
        public async Task UpdatePaymentAmountAsync(int formId)
        {
            var detailSumTotal = await GetFormDetailSumTotalAsync(formId);

            var updateCommand = @"
                UPDATE
                    forms_payment
                SET
                    payment_amount = @PaymentAmount
                WHERE
                    form_id = @FormId";
            var parameters = new { FormId = formId, PaymentAmount = detailSumTotal };
            await _dbConnection.ExecuteAsync(updateCommand, parameters);
        }

        // Delete section

        public async Task DeleteFormAsync(int formId)
        {
            var deleteCommand = @"
                DELETE FROM
                    forms
                WHERE
                    id = @FormId";
            var parameters = new { FormId = formId };
            await _dbConnection.ExecuteAsync(deleteCommand, parameters);
        }
    }
}
