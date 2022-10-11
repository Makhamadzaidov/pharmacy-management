using PharmacyAppExam.WebApi.Models;

namespace PharmacyAppExam.WebApi.Interfaces.Services
{
    public interface IAuthService
    {
        string GenerateToken(User user);
    }
}
