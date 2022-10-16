using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyAppExam.WebApi.Commons.Utils;
using PharmacyAppExam.WebApi.Interfaces.Services;
using PharmacyAppExam.WebApi.ViewModels.Orders;

namespace PharmacyAppExam.WebApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery]PaginationParams @params)
        {
            return Ok(await _orderService.GetAllAsync(expression: null, @params));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]OrderCreateViewModel orderCreateViewModel)
        {
            long userId = long.Parse(HttpContext.User.FindFirst("Id")!.Value);
            return Ok(await _orderService.CreateAsync(userId, orderCreateViewModel));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(await _orderService.DeleteAsync(order => order.Id == id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            long userId = long.Parse(HttpContext.User.FindFirst("Id")!.Value);
            return Ok(await _orderService.GetAsync(userId, order => order.Id == id));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm]OrderCreateViewModel orderCreateViewModel)
        {
            return Ok(await _orderService.UpdateAsync(id, orderCreateViewModel));
        }
    }
}
