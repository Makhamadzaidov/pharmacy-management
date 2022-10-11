using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyAppExam.WebApi.Commons.Utils;
using PharmacyAppExam.WebApi.ViewModels.Drugs;

namespace PharmacyAppExam.WebApi.Controllers
{
    [Route("api/drugs")]
    [ApiController]
    public class DrugsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]DrugCreateViewModel drugCreateViewModel)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm]DrugCreateViewModel drugCreateViewModel)
        {
            return Ok();
        }
    }
}
