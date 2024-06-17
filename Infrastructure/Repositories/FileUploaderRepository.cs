using Dapper;
using Domain.Entities.Commons.FileUploader;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories
{
    public class FileUploaderRepository : IFileUploaderRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IConfiguration _configuration;

        public FileUploaderRepository(IDbConnection dbConnection, IConfiguration configuration)
        {
            _dbConnection = dbConnection;
            _configuration = configuration;
        }

        public async Task<int> CreateFileUrlAsync(FileUploader uploader, IFormFile file)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();
                var transaction = _dbConnection.BeginTransaction();
                try
                {
                    var fileFullPath = await GenerateFilePath(uploader.FormId, file);
                    var writeCommand = @"
                        INSERT INTO forms_filepaths
                            (form_id,
                            uploader_id,
                            file_name,
                            file_path,
                            created_at,
                            updated_at)
                        VALUES
                            (@OrderFormId,
                            @UploaderId,
                            @FileName,
                            @FilePath,
                            @CreateAt,
                            @UpdateAt);
                        SELECT LAST_INSERT_ID();";
                    var parameters = new
                    {
                        OrderFormId = uploader.FormId,
                        UploaderId = uploader.UploaderId,
                        FileName = Path.GetFileName(fileFullPath),
                        FilePath = fileFullPath,
                        CreateAt = DateTime.UtcNow,
                        UpdateAt = DateTime.UtcNow
                    };
                    var fileId = await _dbConnection.ExecuteScalarAsync<int>(writeCommand, parameters, transaction);
                    transaction.Commit();
                    await StoreFileAsync(file, fileFullPath);
                    return fileId;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<List<FileUploader>> GetFilePathAsync(int formId)
        {
            var readCommand = @"
                    SELECT
                        form_id AS FormId,
                        file_id AS FileId,
                        uploader_id AS UploaderId,
                        file_name AS FileName,
                        file_path AS FilePath,
                        created_at AS CreatedAt,
                        updated_at AS UpdatedAt
                    FROM forms_filepaths
                    WHERE form_id = @FromId";
            var parameters = new { FromId = formId };
            var filePath = await _dbConnection.QueryAsync<FileUploader>(readCommand, parameters);
            return filePath.AsList();
        }

        private async Task<string> GenerateFilePath(int formId, IFormFile file)
        {
            var currentMaxIncrement = await GetOrderFormMaxIncrement(formId);

            int autoIncrement = currentMaxIncrement + 1;

            string rootPath = _configuration["FileUploadPath"];
            DateTime now = DateTime.Now;
            string extension = Path.GetExtension(file.FileName);
            string fileName = now.ToString("yyyy-MM-dd") + $"-{autoIncrement}" + extension;
            string filePath = Path.Combine(rootPath, formId.ToString(), fileName);

            return filePath;
        }

        private async Task<int> GetOrderFormMaxIncrement(int orderFormId)
        {
            var readCommand = @"
                SELECT COUNT(file_path)
                FROM forms_filepaths
                WHERE form_id = @OrderFormId";
            var parameters = new { OrderFormId = orderFormId };
            var maxIncrement = await _dbConnection.ExecuteScalarAsync<int>(readCommand, parameters);
            return maxIncrement;
        }

        private async Task StoreFileAsync(IFormFile file, string filePath)
        {
            var directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
    }
}
