using Dapper;
using Domain.Entities.Commons.FileUploader;
using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Data;

namespace Infrastructure.Repositories
{
    public class FileUploaderRepository : IFileUploaderRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;
        private readonly string _procurementDirectory;
        private readonly string _gcsBaseUrl = "https://storage.googleapis.com";

        public FileUploaderRepository(IDbConnection dbConnection, IOptions<GoogleCloudStorageOptions> storageOptions)
        {
            _dbConnection = dbConnection;

            var options = storageOptions.Value;
            _bucketName = options.BucketName;
            _procurementDirectory = options.ProcurementDirectory;

            // Set the Google Cloud credentials environment variable
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", options.CredentialsFilePath);
            _storageClient = StorageClient.Create();
        }

        public async Task<List<int>> CreateFileUrlsAsync(FileUploader uploader, List<IFormFile> files)
        {
            var fileIds = new List<int>();

            using (_dbConnection)
            {
                _dbConnection.Open();
                var transaction = _dbConnection.BeginTransaction();
                try
                {
                    foreach (var file in files)
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
                        fileIds.Add(fileId);
                        await StoreFileAsync(file, fileFullPath);
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }

            return fileIds;
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

            string fileName = $"{DateTime.Now:yyyy-MM-dd}-{autoIncrement}{Path.GetExtension(file.FileName)}";
            string objectName = $"{_procurementDirectory}/{formId}/{fileName}";
            string filePath = $"{_gcsBaseUrl}/{_bucketName}/{objectName}";

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
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                var objectName = filePath.Replace($"{_gcsBaseUrl}/{_bucketName}/", "").Replace("\\", "/");
                var mimeType = GetMimeType(Path.GetExtension(file.FileName));
                await _storageClient.UploadObjectAsync(_bucketName, objectName, mimeType, memoryStream);
            }
        }

        private string GetMimeType(string extension)
        {
            return extension.ToLower() switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".tiff" => "image/tiff",
                ".svg" => "image/svg+xml",
                ".mp4" => "video/mp4",
                ".mov" => "video/quicktime",
                ".avi" => "video/x-msvideo",
                ".mkv" => "video/x-matroska",
                ".wmv" => "video/x-ms-wmv",
                ".flv" => "video/x-flv",
                ".webm" => "video/webm",
                _ => "application/octet-stream"
            };
        }
    }
}
