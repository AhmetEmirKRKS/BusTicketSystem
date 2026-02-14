using Microsoft.Extensions.DependencyInjection;
using BusTicketSystem.Application.Mappings;
using System.Reflection;
using MediatR;
using Mapster;
using MapsterMapper;
using FluentValidation;
using BusTicketSystem.Application.Behaviors;

namespace BusTicketSystem.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            
            //Bu projedeki (application) tüm command ve handler'ları bul ve sisteme ekle diyoruz
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            //Mapster
            services.RegisterMapster(); //yazdığımız config metodunu çağırıyoruz
            services.AddScoped<IMapper, ServiceMapper>();

            services.AddValidatorsFromAssembly(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        }
    }
}
