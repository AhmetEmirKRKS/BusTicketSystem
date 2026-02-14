using BusTicketSystem.Application.DTOs;
using BusTicketSystem.Application.Interfaces.Auth;
using BusTicketSystem.Application.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusTicketSystem.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var validator = new LoginCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors[0].ErrorMessage);

            var users = await _unitOfWork.UserRepository.GetWhereAsync(x => x.Email == request.Email);
            var user = users.FirstOrDefault();

            if (user == null)
                throw new Exception("Kullanıcı bulunamadı veya şifre hatalı");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

            if (!isPasswordValid)
                throw new Exception("Kullanıcı bulunamadı veya şifre hatalı");

            if (!user.IsEmailVerified)
                throw new Exception("Lütfen önce e-posta adresinizi doğrulayın");

            var token = _tokenService.GenerateToken(user);

            return new AuthResponse
            {
                Id = user.Id,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                Token = token
            };
        }
    }
}
