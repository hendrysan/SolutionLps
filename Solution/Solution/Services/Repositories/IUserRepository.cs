using Solution.Models;

namespace Solution.Services.Repositories
{
    public interface IUserRepository
    {
        Task<DefaultResponse> Register(string email, string password);
        Task<LoginResponse> Login(string email, string password);
        
    }
}
