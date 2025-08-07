using Microsoft.AspNetCore.Http;

namespace Social.Application.Services
{
    public interface IFileUploadService
    {
        Task<(string Url, string FileName)> UploadFileAsync(IFormFile file, string folder = "posts");
        Task<bool> DeleteFileAsync(string fileName, string folder = "posts");
    }
}
