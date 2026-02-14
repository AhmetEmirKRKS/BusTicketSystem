using BusTicketSystem.Application.Interfaces.UnitOfWork;
using BusTicketSystem.Domain.Entities;
using MediatR;
using MapsterMapper;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace BusTicketSystem.Application.Features.Buses.Commands.CreateBus
{
    public class CreateBusCommandHandler : IRequestHandler<CreateBusCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateBusCommand request, CancellationToken cancellationToken)
        {
            var newBus = _mapper.Map<Bus>(request);

            await _unitOfWork.BusRepository.AddAsync(newBus);
            await _unitOfWork.SaveChangesAsync();

            return newBus.Id;
        }
    }
}
