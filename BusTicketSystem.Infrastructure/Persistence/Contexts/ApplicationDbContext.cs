using BusTicketSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusTicketSystem.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options) { }

        public DbSet<Bus> Buses { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }

        //Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Trip>()
                .Property(t => t.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Ticket>()
                .Property(t => t.PricePaid)
                .HasPrecision(18, 2);


            base.OnModelCreating(modelBuilder);
        }
    }
}
