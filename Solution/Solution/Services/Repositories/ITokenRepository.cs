using System.Security.Claims;

namespace Solution.Services.Repositories
{
    public interface ITokenRepository
    {
        List<Claim> CreateClaims(string id, string user);
    }
}
