using BusTicketSystem.Application.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace BusTicketSystem.Application.Features.Auth.Commands.VerifyEmail
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public VerifyEmailCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.UserRepository.GetWhereAsync(x => x.Email == request.Email);
            var user = users.FirstOrDefault();

            if (user == null) throw new Exception("Kullanıcı bulunamadı");

            if (user.IsEmailVerified) return "E-posta zaten doğrulanmış";

            if (user.EmailVerificationCode != request.Code)
                throw new Exception("Hatalı doğrulama kodu");

            user.IsEmailVerified = true;
            user.EmailVerificationCode = null; //kodu temizle

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return "E-posta başarıyla doğrulandı!Şimdi giriş yapabilirsiniz";
        }
    }
}
