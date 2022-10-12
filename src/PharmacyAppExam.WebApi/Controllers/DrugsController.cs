using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyAppExam.WebApi.Commons.Utils;
using PharmacyAppExam.WebApi.Interfaces.Services;
using PharmacyAppExam.WebApi.ViewModels.Drugs;

namespace PharmacyAppExam.WebApi.Controllers
{
    [Route("api/drugs")]
    [ApiController]
    public class DrugsController : ControllerBase
    {
        private readonly IDrugService _drugService;

        public DrugsController(IDrugService drugService)
        {
            _drugService = drugService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        {
            return Ok(await _drugService.GetAllAsync(expression: null, @params));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]DrugCreateViewModel drugCreateViewModel)
        {
            return Ok(await _drugService.CreateAsync(drugCreateViewModel));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(await _drugService.DeleteAsync(drug => drug.Id == id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            return Ok(await _drugService.GetAsync(drug => drug.Id == id));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm]DrugCreateViewModel drugCreateViewModel)
        {
            return Ok(await _drugService.UpdateAsync(id, drugCreateViewModel));
        }
    }
}
