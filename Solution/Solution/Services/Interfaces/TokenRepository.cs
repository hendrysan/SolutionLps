using Solution.Services.Repositories;
using System.Security.Claims;

namespace Solution.Services.Interfaces
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;

        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public List<Claim> CreateClaims(string id, string user)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim("Id", id),
                    new Claim("Name", user),
                    new Claim("LastAccess", DateTime.Now.ToString())
                };

                return claims;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
