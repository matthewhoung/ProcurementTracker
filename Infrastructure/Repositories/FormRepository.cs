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
            return formId;
        }
        public async Task<int> CreateOrderFormAsync(int formId)
        {
            var writeCommand = @"
                    INSERT INTO order_forms
                    (
                        form_id
                    )
                    VALUES
                    (
                        @FormId
                    );
                    SELECT LAST_INSERT_ID();";
            var parameters = new
            {
                FormId = formId
            };

            var orderFormId = await _dbConnection.ExecuteScalarAsync<int>(writeCommand, parameters);
            return orderFormId;
        }

        public async Task<int> CreateReceiveFormAsync(int formId)
        {
            var writeCommand = @"
                    INSERT INTO receive_forms
                    (
                        form_id
                    )
                    VALUES
                    (
                        @FormId
                    );
                    SELECT LAST_INSERT_ID();";
            var parameters = new
            {
                FormId = formId
            };

            var receiveFormId = await _dbConnection.ExecuteScalarAsync<int>(writeCommand, parameters);
            return receiveFormId;
        }

        public async Task<int> CreatePayableFormAsync(int formId)
        {
            var writeCommand = @"
                    INSERT INTO payable_forms
                    (
                        form_id
                    )
                    VALUES
                    (
                        @FormId
                    );
                    SELECT LAST_INSERT_ID();";
            var parameters = new
            {
                FormId = formId
            };

            var payableFormId = await _dbConnection.ExecuteScalarAsync<int>(writeCommand, parameters);
            return payableFormId;
        }

        public async Task<int> CreateFormDetailAsync(FormDetail formDetail)
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
                    );
                    SELECT LAST_INSERT_ID();";
            var details = new
            {
                formDetail.FormId,
                formDetail.Title,
                formDetail.Description,
                formDetail.Quantity,
                formDetail.UnitPrice,
                formDetail.UnitId,
                formDetail.Total,
                formDetail.IsChecked
            };

            var detailId = await _dbConnection.ExecuteScalarAsync<int>(writeCommand, details);
            return detailId;
        }

        public async Task<int> CreateFormSignatureMemberAsync(FormSignatureMember formSignatureMember)
        {
            var table = formSignatureMember.Stage switch
            {
                "order" => "order_signatures",
                "receive" => "receive_signatures",
                "payable" => "payable_signatures",
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
                    );
                    SELECT LAST_INSERT_ID();";
            var parameters = new
            {
                formSignatureMember.FormId,
                formSignatureMember.UserId,
                formSignatureMember.RoleId,
                formSignatureMember.IsChecked
            };
            var signId = await _dbConnection.ExecuteScalarAsync<int>(writeCommand, parameters);
            return signId;
        }

        public async Task<int> CreateFormWorkerList(FormWorker formWorker)
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
                    );
                    SELECT LAST_INSERT_ID();";
            var parameters = new
            {
                formWorker.FormId,
                formWorker.WorkerTypeId,
                formWorker.WorkerTeamId
            };
            var workerId = await _dbConnection.ExecuteScalarAsync<int>(writeCommand, parameters);
            return workerId;
        }

        public async Task<int> CreateFormPaymentInfo(FormPayment formPaymentInfo)
        {
            var writeCommand = @"
                    INSERT INTO forms_payment
                    (
                        form_id,
                        payment_total,
                        payment_delta,
                        payment_amount,
                        payment_title_id,
                        payment_tool_id
                    )
                    VALUES
                    (
                        @FormId,
                        @PaymentTotal,
                        @PaymentDelta,
                        @PaymentAmount,
                        @PaymentTitleId,
                        @PaymentToolId
                    );
                    SELECT LAST_INSERT_ID();";
            var parameters = new
            {
                formPaymentInfo.FormId,
                formPaymentInfo.PaymentTotal,
                formPaymentInfo.PaymentDelta,
                formPaymentInfo.PaymentAmount,
                formPaymentInfo.PaymentTitleId,
                formPaymentInfo.PaymentToolId
            };
            var payId = await _dbConnection.ExecuteScalarAsync<int>(writeCommand, parameters);
            return payId;
        }

        public async Task<int> CreateFormDepartment(FormDepartment formDepartment)
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
                    );
                    SELECT LAST_INSERT_ID();";

            var parameters = new
            {
                formDepartment.FormId,
                formDepartment.DepartmentId
            };

            var departmentId = await _dbConnection.ExecuteScalarAsync<int>(writeCommand, parameters);
            return departmentId;
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

        public async Task<List<FormSignatureMember>> GetFormSignatureMembersByFormIdAsync(int formId)
        {
            var stageQuery = @"
                SELECT stage
                FROM forms
                WHERE id = @FormId";
            var parameters = new { FormId = formId };
            var stage = await _dbConnection.QuerySingleOrDefaultAsync<string>(stageQuery, parameters);

            if (string.IsNullOrEmpty(stage))
            {
                throw new ArgumentException("Invalid form Id");
            }

            string signatureTable = stage switch
            {
                "OrderForm" => "order_signatures",
                "ReceiveForm" => "receive_signatures",
                "PayableForm" => "payable_signatures",
                _ => throw new ArgumentException("Invalid form stage")
            };

            var readCommand = $@"
                SELECT
                    fsm.sign_id AS SignId,
                    f.id AS FormId,
                    fsm.user_id AS UserId,
                    r.role_id AS RoleId,
                    r.role_name AS RoleName,
                    fsm.is_checked AS IsChecked
                FROM
                    forms f
                JOIN
                    {signatureTable} fsm ON f.id = fsm.form_id
                JOIN
                    roles r ON fsm.role_id = r.role_id
                WHERE
                    f.id = @FormId";

            var formSignatureMembers = await _dbConnection.QueryAsync<FormSignatureMember>(readCommand, parameters);
            return formSignatureMembers.AsList();
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
                        fp.payment_amount AS PaymentAmount,
                        fp.payment_title_id AS PaymentTitleId,
                        pt.pay_type_name AS PaymentTitle,
                        fp.payment_tool_id AS PaymentToolId,
                        pb.pay_by_name AS PaymentTool
                    FROM
                        forms f
                    JOIN
                        forms_payment fp ON f.id = fp.form_id
                    JOIN
                        pay_types pt ON fp.payment_title_id = pt.pay_type_id
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
        public async Task UpdateSignatureAsync(FormSignatureMember formSignatureMember)
        {
            var table = formSignatureMember.Stage switch
            {
                "order" => "order_signatures",
                "receive" => "receive_signatures",
                "payable" => "payable_signatures",
                _ => throw new ArgumentException("Invalid stage")
            };

            var updateCommand = $@"
                    UPDATE
                        {table}
                    SET
                        is_checked = 1
                    WHERE
                        form_id = @FormId
                    AND
                        user_id = @UserId";
            var parameters = new
            {
                FormId = formSignatureMember.FormId,
                UserId = formSignatureMember.UserId
            };
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
