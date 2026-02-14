using BusTicketSystem.Application.DTOs;
using BusTicketSystem.Application.Interfaces.UnitOfWork;
using MapsterMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed; //redis kütüphanesi
using System.Text.Json;

namespace BusTicketSystem.Application.Features.Trips.Queries.GetAllTrips
{
    public class GetAllTripsQueryHandler : IRequestHandler<GetAllTripsQuery, List<TripDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public GetAllTripsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<List<TripDto>> Handle(GetAllTripsQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = "all_trips_list"; //cache için benzersiz bir anahtar

            //veri var mı diye redise bakyoruz
            string? cachedData = await _cache.GetStringAsync(cacheKey, cancellationToken);

            //varsa direkt dto listesi olarak döndürüyoruz
            if (!string.IsNullOrEmpty(cachedData))
            {
                 var result = JsonSerializer.Deserialize<List<TripDto>>(cachedData);
                 if (result != null)
                    return result;
            }

            //Burda join atıyoruz
            var trips = await _unitOfWork.TripRepository.GetAllAsync(
                x => x.Bus,
                x => x.Route
                );

            var tripDtos = _mapper.Map<List<TripDto>>(trips);

            //çektiğimiz veriyi redise kaydediyoruz
            var cachedOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10), //10 dakika dursun
                SlidingExpiration = TimeSpan.FromMinutes(2) //2 dakika kimse sormazsa silinsin
            };

            //dto listesini json yapıp saklıyoruz
            string serializedData = JsonSerializer.Serialize(tripDtos);

            await _cache.SetStringAsync(cacheKey, serializedData, cachedOptions, cancellationToken);

            return _mapper.Map<List<TripDto>>(trips);
        }
    }
}
