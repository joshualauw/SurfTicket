
using SurfTicket.Infrastructure.Helpers;

namespace SurfTicket.Infrastructure.FileStorage
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;
        public FileStorageService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string> SaveFileAsync(IFormFile file, string folder)
        {
            var uploadsPath = Path.Combine(_env.WebRootPath, "Uploads", folder);
            Directory.CreateDirectory(uploadsPath);

            var randCode = GenCodeHelper.GenerateCode();
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var fileName = $"{folder.ToLower()}_{randCode}_{timestamp}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsPath, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return Path.Combine("Uploads", folder, fileName).Replace("\\", "/");
        }
    }
}
