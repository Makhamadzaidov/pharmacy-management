using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyAppExam.WebApi.Commons.Utils;
using PharmacyAppExam.WebApi.Interfaces.Services;
using PharmacyAppExam.WebApi.ViewModels.Users;

namespace PharmacyAppExam.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        {
            return Ok(await _userService.GetAllAsync(expression: null, @params));
        }

        [HttpDelete("{id}"), Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> DeleteAsync()
        {
            long id = long.Parse(HttpContext.User.FindFirst("Id")?.Value ?? "0");
            return Ok(await _userService.DeleteAsync(user => user.Id == id));
        }

        [HttpPut, Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> UpdateAsync([FromForm] UserCreateViewModel userCreateViewModel)
        {
            long id = long.Parse(HttpContext.User.FindFirst("Id")?.Value ?? "0");
            return Ok(await _userService.UpdateAsync(id, userCreateViewModel));
        }

        [HttpGet("Info"), Authorize(Roles = "User")]
        public async Task<IActionResult> GetInfoAsync()
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id")?.Value ?? "0");

            return Ok(await _userService.GetAsync(user => user.Id == id));
        }
    }
}
