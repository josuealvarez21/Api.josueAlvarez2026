using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SexShopApi.DTOs;
using SexShopApi.Services;
using System.Security.Claims;

namespace SexShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateOrderDto dto)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized();

            int userId = int.Parse(userIdString);
            var order = await _orderService.CreateOrderAsync(userId, dto);
            return Ok(order);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetMyOrders()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized();

            int userId = int.Parse(userIdString);
            var orders = await _orderService.GetUserOrdersAsync(userId);
            return Ok(orders);
        }
    }
}
