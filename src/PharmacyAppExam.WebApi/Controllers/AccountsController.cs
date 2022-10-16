using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyAppExam.WebApi.Interfaces.Services;
using PharmacyAppExam.WebApi.ViewModels.Users;

namespace PharmacyAppExam.WebApi.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountServcie;

        public AccountsController(IAccountService accountService)
        {
            _accountServcie = accountService;
        }

        [HttpPost("registr"), AllowAnonymous]
        public async Task<IActionResult> CreateAsync([FromForm]UserCreateViewModel user)
        {
            return Ok(await _accountServcie.RegistrAsync(user));
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromForm]UserLoginModel user)
        {
            return Ok(new {Token = await _accountServcie.LoginAsync(user) });
        }

        [HttpPost("emailVerify"), AllowAnonymous]
        public async Task<IActionResult> EmailVerify([FromForm]EmailAddress emailAddress)
        {
            return Ok(new { Token = await _accountServcie.EmailVerifyAsync(emailAddress) });
        }
    }
}
