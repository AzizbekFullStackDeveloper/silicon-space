using Microsoft.AspNetCore.Http;
using SiliconSpace.Service.Exceptions;
using SiliconSpace.Service.Helpers;
using SiliconSpace.Service.Interfaces;

namespace SiliconSpace.Service.Services
{
    public class FileUploadService : IFileUploadService
    {
        public async Task<bool> FileDeleteAsync(string filePath)
        {
            var fullPath = Path.Combine(WebEnvironmentHost.WebRootPath, filePath);
            if (fullPath != null)
            {
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
            }

            return false;
        }

        public async Task<string> FileUploadAsync(IFormFile FileData, string FileFolderName)
        {
            if (FileData == null)
            {
                throw new SiliconSpaceException(400, "File is null");
            }
            var WwwRootPath = Path.Combine(WebEnvironmentHost.WebRootPath, "Assets", FileFolderName);

            if (!Directory.Exists(WwwRootPath))
            {
                Directory.CreateDirectory(WwwRootPath);
            }
            var GuidName = new Guid().ToString();
            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(FileData.FileName);
            var fullPath = Path.Combine(WwwRootPath, fileName);

            using (var streamFile = File.OpenWrite(fullPath))
            {
                await FileData.CopyToAsync(streamFile);
            }
            var ResultPath = Path.Combine("Assets", FileFolderName, fileName);
            return ResultPath;
        }
    }
}
