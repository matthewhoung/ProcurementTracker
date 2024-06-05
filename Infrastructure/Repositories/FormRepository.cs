using Dapper;
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
                        is_check
                    )
                    VALUES
                    (
                        @FormId,
                        @Name,
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
                        detail_id AS DetailId,
                        form_id AS FormId,
                        detail_title AS Title,
                        detail_description AS Description,
                        quantity AS Quantity,
                        unit_price AS UnitPrice,
                        unit_id AS UnitId,
                        detail_total AS Total,
                        is_check AS IsChecked
                    FROM
                        forms_detail
                    WHERE
                        form_id = @FormId";
            var parameters = new { FormId = formId };
            var formDetails = await _dbConnection.QueryAsync<FormDetail>(readCommand, parameters);
            return formDetails.AsList();
        }
    }
}
