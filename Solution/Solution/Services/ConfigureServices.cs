using Solution.Services.Interfaces;
using Solution.Services.Repositories;

namespace Solution.Services
{
    public static class ConfigureServices
    {

        public static IConfiguration? Configuration { get; }
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            return services;
        }
    }
}
