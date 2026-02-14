using BusTicketSystem.Application.Interfaces.Repositories;
using BusTicketSystem.Application.Interfaces.UnitOfWork;
using BusTicketSystem.Domain.Entities;
using BusTicketSystem.Infrastructure.Persistence.Contexts;
using System.Threading.Tasks;

namespace BusTicketSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGenericRepository<Bus> BusRepository { get; }
        public IGenericRepository<Trip> TripRepository { get; }
        public IGenericRepository<Ticket> TicketRepository { get; }
        public IGenericRepository<Passenger> PassengerRepository { get; }
        public IGenericRepository<Route> RouteRepository { get; }
        public IGenericRepository<User> UserRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            //Her çağrıldığında yeni repository üretmek yerine, var olan Context ile üretiyoruz
            BusRepository = new GenericRepository<Bus>(_context);
            TripRepository = new GenericRepository<Trip>(_context);
            TicketRepository = new GenericRepository<Ticket>(_context);
            PassengerRepository = new GenericRepository<Passenger>(_context);
            RouteRepository = new GenericRepository<Route>(_context);
            UserRepository = new GenericRepository<User>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
