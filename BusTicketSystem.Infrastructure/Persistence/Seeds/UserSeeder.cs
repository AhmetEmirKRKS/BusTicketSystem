using BusTicketSystem.Domain.Entities;
using BusTicketSystem.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BusTicketSystem.Infrastructure.Persistence.Seeds
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var adminExists = await context.Users.AnyAsync(u => u.Role == "Admin");

            if(!adminExists)
            {
                var adminUser = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "System",
                    LastName = "Admin",
                    Email = "admin@gmail.com",
                    Role = "Admin",
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    Password = BCrypt.Net.BCrypt.HashPassword("Admin.123")
                };

                await context.Users.AddAsync(adminUser);
                await context.SaveChangesAsync();

                Console.WriteLine("---> SEED: Default Admin kullanıcısı oluşturuldu");
            }
        }
    }
}
