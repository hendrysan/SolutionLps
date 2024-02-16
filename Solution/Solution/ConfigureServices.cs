using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json.Serialization;
using Solution.Contexts;
using Solution.Models;
using Solution.Services;

namespace Solution
{
    public static class ConfigureServices
    {
        //public static IConfiguration? _configuration { get; }
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddMvcCore().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    //var message = context

                    var result = new ErrorModel()
                    {
                        IsSuccess = false,
                        ErrorCode = 400,
                        Message = "Bad Request",
                        Data = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList()
                    };
                    return new BadRequestObjectResult(result);
                };
            });

            //services.AddIdentityCore<MasterUserModel>(options =>
            //{
            //    options.SignIn.RequireConfirmedAccount = false;
            //    options.User.RequireUniqueEmail = true;
            //    options.Password.RequireDigit = true;
            //    options.Password.RequiredLength = 8;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireLowercase = false;
            //}).AddEntityFrameworkStores<DatabaseContext>();

            
            services.AddDbContext<DatabaseContext>(options =>
            {
            });

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                // enables immediate logout, after updating the user's stat.
                options.ValidationInterval = TimeSpan.Zero;
            });

            services.AddHttpContextAccessor();
            services.AddRepositoryServices();

            return services;
        }
    }
}
