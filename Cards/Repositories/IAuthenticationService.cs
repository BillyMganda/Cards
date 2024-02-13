using Cards.DTOs;
using Cards.Models;

namespace Cards.Repositories
{
    public interface IAuthenticationService
    {
        Task<User?> GetUserAsync(string email);
        Task CreateUserAsync(CreateUserRequest request);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
        Task<string?> LoginAsync(LoginRequest request);
    }
}
