using BusTicketSystem.Application.DTOs;
using BusTicketSystem.Application.Interfaces.UnitOfWork;
using MapsterMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BusTicketSystem.Application.Features.Routes.Queries.GetAllRoutes
{
    public class GetAllRoutesQueryHandler : IRequestHandler<GetAllRoutesQuery, List<RouteDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllRoutesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<RouteDto>> Handle(GetAllRoutesQuery request, CancellationToken cancellationToken)
        {
            var routes = await _unitOfWork.RouteRepository.GetAllAsync();
            return _mapper.Map<List<RouteDto>>(routes);
        }
    }
}
