using SexShopApi.DTOs;
using SexShopApi.Models;

namespace SexShopApi.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(RegisterDto dto);
        Task<string?> LoginAsync(LoginDto dto);
    }
}
