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
    }
}
