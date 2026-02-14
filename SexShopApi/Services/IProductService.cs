using SexShopApi.DTOs;
using SexShopApi.Models;

namespace SexShopApi.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync(string? category, decimal? maxPrice, int page = 1, int pageSize = 10);
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(CreateProductDto dto);
        Task UpdateAsync(int id, CreateProductDto dto);
        Task DeleteAsync(int id);
    }
}
