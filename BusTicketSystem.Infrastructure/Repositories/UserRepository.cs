using BusTicketSystem.Application.Interfaces.Repositories;
using BusTicketSystem.Domain.Entities;
using BusTicketSystem.Infrastructure.Persistence.Contexts;

namespace BusTicketSystem.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context): base(context) { }
    }
}
