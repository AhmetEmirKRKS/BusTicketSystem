using BusTicketSystem.Domain.Common;

namespace BusTicketSystem.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; } = false; //varsayılan: doğrulanmadı
        public string? EmailVerificationCode { get; set; } //6 haneli kod
        public User(string firstName, string lastName, string email, string password, string role)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Role = role;
        }

        public User() { }
    }
}
