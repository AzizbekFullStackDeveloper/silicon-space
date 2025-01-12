using Microsoft.AspNetCore.Http;

namespace SiliconSpace.Service.Interfaces
{
    public interface IFileUploadService
    {
        public Task<string> FileUploadAsync (IFormFile FileData, string FileFolderName);
        public Task<bool> FileDeleteAsync(string filePath);
    }
}
