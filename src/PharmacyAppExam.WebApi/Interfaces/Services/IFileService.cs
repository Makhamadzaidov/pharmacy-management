namespace PharmacyAppExam.WebApi.Interfaces.Services
{
    public interface IFileService
    {
        Task<string> SaveImageAsync(IFormFile image);
        Task<bool> DeleteImageAsync(string relativeFilePath);
    }
}
