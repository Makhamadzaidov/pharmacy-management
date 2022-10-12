using PharmacyAppExam.WebApi.ViewModels.Users;

namespace PharmacyAppExam.WebApi.Interfaces.Services
{
    public interface IAccountService
    {
        Task<string> EmailVerifyAsync(EmailAddress emailAddress);
        Task<bool> RegistrAsync(UserCreateViewModel userCreateViewModel);
        Task<string> LoginAsync(UserLoginModel userLoginModel);
    }
}
