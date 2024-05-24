using DatingApp.Data;
using DatingApp.Interface;
using DatingApp.Services;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Extensions
{

    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {

            //datacontext added for add migration command
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("APIConnectionString"));
            });

            services.AddCors();
            // This is  used to add the interface and the token services method
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
