namespace SurfTicket.Infrastructure.FileStorage
{
    public interface IFileStorageService
    {
        public Task<string> SaveFileAsync(IFormFile file, string folder);
    }
}
