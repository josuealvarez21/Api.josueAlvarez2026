using Microsoft.EntityFrameworkCore;
using SexShopApi.Data;
using SexShopApi.DTOs;
using SexShopApi.Models;
using SexShopApi.Repositories;

namespace SexShopApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;

        // Note: For filtering/pagination, generic repository might be limited. 
        // We can cast to DbContext or extend Repository. 
        // For simplicity, I'll assume we can use FindAsync or just access context if needed, 
        // but let's try to stick to Repository pattern or just use simple logic.
        // Actually, for advanced filtering, better to have specific repository method or expose IQueryable (leaky).
        // I will implement simple in-memory filtering after fetching all? No, inefficient.
        // I'll modify Repository to expose IQueryable or just inject DbContext here since Service is part of Application layer 
        // and usually knows about EF in this simple architecture, or I'll just use the Repository methods I defined.
        
        // I'll access DbContext directly for complex queries or extend Repository.
        // Let's us DbContext directly here as it's easier for this scope, 
        // OR add specific methods to IProductRepository.
        // Given constraints, I'll access DbContext via a new specific repository implementation 
        // OR just use the generic one and filter in memory if creating a specific repo is too much boilerplate.
        // I'll add a specific method to the Service that uses the generic FindAsync which takes a predicate.
        
        // Wait, `FindAsync` takes `Expression<Func<T, bool>>`. That's good for simple usage.
        // But for pagination I need Skip/Take.
        
        private readonly AppDbContext _context;

        public ProductService(IRepository<Product> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(string? category, decimal? maxPrice, int page = 1, int pageSize = 10)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.Category.Contains(category));

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Product> CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                Category = dto.Category,
                ImageUrl = dto.ImageUrl,
                IsActive = true
            };

            await _repository.AddAsync(product);
            return product;
        }

        public async Task UpdateAsync(int id, CreateProductDto dto)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return;

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.Category = dto.Category;
            product.ImageUrl = dto.ImageUrl;

            await _repository.UpdateAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
