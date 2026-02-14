using SexShopApi.DTOs;
using SexShopApi.Models;

namespace SexShopApi.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(int userId, CreateOrderDto dto);
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<IEnumerable<Order>> GetUserOrdersAsync(int userId);
    }
}
