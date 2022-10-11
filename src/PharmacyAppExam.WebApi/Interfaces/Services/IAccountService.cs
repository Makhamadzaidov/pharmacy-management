using PharmacyAppExam.WebApi.ViewModels.Users;

namespace PharmacyAppExam.WebApi.Interfaces.Services
{
    public interface IAccountService
    {
        Task<string> EmailVerify(EmailAddress emailAddress);
        Task<bool> RegistrAsync(UserCreateViewModel userCreateViewModel);
        Task<string> LoginAsync(UserLoginModel userLoginModel);
    }
}
