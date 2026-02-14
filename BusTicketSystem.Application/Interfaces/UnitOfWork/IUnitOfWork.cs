using BusTicketSystem.Application.Interfaces.Repositories;
using BusTicketSystem.Domain.Entities;
using System.Threading.Tasks;

namespace BusTicketSystem.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        //her entity için bir repository tanımlıyoruz
        IGenericRepository<Bus> BusRepository { get; }
        IGenericRepository<Trip> TripRepository { get; }
        IGenericRepository<Ticket> TicketRepository { get; }
        IGenericRepository<Passenger> PassengerRepository { get; }
        IGenericRepository<Route> RouteRepository { get; }
        IGenericRepository<User> UserRepository { get; }

        Task<int> SaveChangesAsync(); //veritabanına yazma işlemi
    }
}
