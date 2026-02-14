using BusTicketSystem.Application.DTOs;
using BusTicketSystem.Application.Interfaces.UnitOfWork;
using MapsterMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BusTicketSystem.Application.Features.Buses.Queries.GetAllBuses
{
    public class GetAllBusesQueryHandler : IRequestHandler<GetAllBusesQuery, List<BusDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllBusesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<BusDto>> Handle(GetAllBusesQuery request, CancellationToken cancellationToken)
        {
            var buses = await _unitOfWork.BusRepository.GetAllAsync();

            return _mapper.Map<List<BusDto>>(buses);
        }
    }
}
