namespace PharmacyAppExam.WebApi.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendAsync(string email, string message);
    }
}
