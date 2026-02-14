using BusTicketSystem.Domain.Common;
using System;
using BusTicketSystem.Domain.Enums;

namespace BusTicketSystem.Domain.Entities
{
    public class Passenger : BaseEntity
    {
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string NationalId { get; private set; } = string.Empty; //tc kimlik no
        public Gender Gender { get; private set; }
        public string PhoneNumber { get; private set; } = string.Empty;
        public string? Email { get; private set; } //opsiyonel olabilir

        public Passenger() { }
        public Passenger(string firstName, string lastName, string nationalId, Gender gender, string phoneNumber, string? email = null)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                throw new Exception("Ad ve soyad boş olamaz");
            if (nationalId.Length != 11)
                throw new Exception("Tc kimlik No 11 haneli olmalıdır");
            if (gender == Gender.None)
                throw new Exception("Cinsiyet seçimi zorunludur");

            FirstName = firstName;
            LastName = lastName;
            NationalId = nationalId;
            Gender = gender;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public void UpdateName(string newFirstName, string newLastName)
        {
            if (string.IsNullOrWhiteSpace(newFirstName) || string.IsNullOrWhiteSpace(newLastName))
                throw new Exception("Ad ve soyad boş olamaz");

            FirstName = newFirstName;
            LastName = newLastName;
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
