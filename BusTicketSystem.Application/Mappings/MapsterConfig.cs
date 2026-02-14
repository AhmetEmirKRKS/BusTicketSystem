using BusTicketSystem.Application.Features.Buses.Commands.CreateBus;
using BusTicketSystem.Domain.Entities;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using BusTicketSystem.Application.DTOs;
using BusTicketSystem.Application.Features.Trips.Commands.CreateTrip;

namespace BusTicketSystem.Application.Mappings
{
    public static class MapsterConfig
    {
        public static void RegisterMapster(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            
            //Global Ayarlar (property isimleri büyük/küçük harf duyarsız olsun)
            config.Default.PreserveReference(true);

            //özel mapping kuralları buraya(command -> entity)
            config.NewConfig<CreateBusCommand, Bus>()
                .Map(dest => dest.SeatLayout, src => src.SeatLayout) //enum eşleşmesi
                .ConstructUsing(src => new Bus(
                    src.PlateNumber,
                    src.Model,
                    src.TotalSeats,
                    src.SeatLayout
                 ));

            config.NewConfig<Bus, BusDto>()
                .Map(dest => dest.SeatLayout, src => src.SeatLayout.ToString());

            config.NewConfig<Route, RouteDto>();

            config.NewConfig<Trip, TripDto>()
                .Map(dest => dest.BusPlateNumber, src => src.Bus.PlateNumber)
                .Map(dest => dest.BusModel, src => src.Bus.Model)
                .Map(dest => dest.RouteName, src => $"{src.Route.DepartureCity} > {src.Route.ArrivalCity}")
                .Map(dest => dest.DurationMinutes, src => src.Route.EstimatedDurationMinutes);


            //Entitylerdeki bus entity'si "set"leri private olduğu için ve constructor zorunlu olduğu için mapster'a "bu nesneyi constructor ile oluştur" diyoruz
            services.AddSingleton(config);
        }
    }
}
