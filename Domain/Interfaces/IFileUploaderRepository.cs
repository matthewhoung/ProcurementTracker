using Domain.Entities.Commons.FileUploader;
using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces
{
    public interface IFileUploaderRepository
    {
        Task<List<int>> CreateFileUrlsAsync(FileUploader uploader, List<IFormFile> files);
        Task<List<FileUploader>> GetFilePathAsync(int formId);
    }
}
