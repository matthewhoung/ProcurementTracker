using Domain.Entities.Commons.FileUploader;
using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces
{
    public interface IFileUploaderRepository
    {
        Task<int> CreateFileUrlAsync(FileUploader uploaderId, IFormFile file);
        Task<List<FileUploader>> GetFilePathAsync(int formId);
    }
}
