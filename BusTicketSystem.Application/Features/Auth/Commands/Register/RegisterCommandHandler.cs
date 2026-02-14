using BusTicketSystem.Application.DTOs;
using BusTicketSystem.Application.Interfaces.Auth;
using BusTicketSystem.Application.Interfaces.UnitOfWork;
using BusTicketSystem.Domain.Entities;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;
using BCrypt.Net;
using BusTicketSystem.Application.Interfaces.Infrastructure;
using Hangfire;

namespace BusTicketSystem.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public RegisterCommandHandler(IUnitOfWork unitOfWork, IEmailService emailService, IBackgroundJobClient backgroundJobClient)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var validator = new RegisterCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if(!validationResult.IsValid)
            {
                throw new Exception(validationResult.Errors[0].ErrorMessage);
            }

            var existingUser = await _unitOfWork.UserRepository.GetWhereAsync(x => x.Email == request.Email);
            if (existingUser.Count > 0)
                throw new Exception("Bu e-posta adresi zaten kullanılıyor");

            //hashleme(kriptolama)
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            //OTP kodu üreticez
            Random generator = new Random();
            string otpCode = generator.Next(100000, 999999).ToString();

            var user = new User(request.FirstName, request.LastName, request.Email, passwordHash, "User")
            {
                IsEmailVerified = false,
                EmailVerificationCode = otpCode
            };

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            //Hangfire arkada ıemailservice'i bulur ve milisaniyeler içinde mail atma işi kuyruğa girer
            _backgroundJobClient.Enqueue(() => _emailService.SendEmailAsync(
                user.Email,
                "BusTicketSystem doğrulama kodu",
                $"Kodunuz: {otpCode}"
                ));

            return new AuthResponse
            {
                Id = user.Id,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                Token = "Lütfen e-postanızı doğrulayınız"
            };
        }
    }
}
