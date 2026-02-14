using BusTicketSystem.Application.Interfaces.UnitOfWork;
using BusTicketSystem.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BusTicketSystem.Infrastructure.Repositories;
using BusTicketSystem.Application.Interfaces.Auth;
using BusTicketSystem.Infrastructure.Services;
using BusTicketSystem.Application.Interfaces.Infrastructure;

namespace BusTicketSystem.Infrastructure
{
    public static class ServiceRegistration
    {
        //"this IServiceCollection services" diyerek .net'in servis havuzunu genişletiyoruz
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                npqsqlOptions =>
                {
                    //POLLY mantığını buraya yazıyoruz
                    npqsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: System.TimeSpan.FromSeconds(30),
                        errorCodesToAdd: null);
                }
            ));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, MockEmailService>();
        }
    }
}
