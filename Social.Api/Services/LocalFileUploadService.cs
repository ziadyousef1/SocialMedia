using Social.Application.Services;

namespace Social.Api.Services
{
    public class LocalFileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<LocalFileUploadService> _logger;

        public LocalFileUploadService(IWebHostEnvironment environment, ILogger<LocalFileUploadService> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        public async Task<(string Url, string FileName)> UploadFileAsync(IFormFile file, string folder = "posts")
        {
            try
            {
                var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads", folder);
                Directory.CreateDirectory(uploadsPath);

                var fileExtension = Path.GetExtension(file.FileName);
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(uploadsPath, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                var url = $"/uploads/{folder}/{fileName}";
                return (url, fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file: {FileName}", file.FileName);
                throw;
            }
        }

        public async Task<bool> DeleteFileAsync(string fileName, string folder = "posts")
        {
            try
            {
                var filePath = Path.Combine(_environment.WebRootPath, "uploads", folder, fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file: {FileName}", fileName);
                return false;
            }
        }
    }
}
