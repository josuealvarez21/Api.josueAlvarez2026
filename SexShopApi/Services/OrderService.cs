using Microsoft.EntityFrameworkCore;
using SexShopApi.Data;
using SexShopApi.DTOs;
using SexShopApi.Models;

namespace SexShopApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(int userId, CreateOrderDto dto)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                OrderDetails = new List<OrderDetail>()
            };

            decimal totalAmount = 0;

            foreach (var item in dto.OrderDetails)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null) continue; // Or throw exception

                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                };

                totalAmount += orderDetail.Quantity * orderDetail.UnitPrice;
                order.OrderDetails.Add(orderDetail);
                
                // Optional: Reduce stock
                // product.Stock -= item.Quantity;
            }

            order.TotalAmount = totalAmount;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Include(o => o.User)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}
