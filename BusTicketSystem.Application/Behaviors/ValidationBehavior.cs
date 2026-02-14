using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusTicketSystem.Application.Behaviors
{
    //Bu sınıf, MediatR pipeline'ına giren her isteği yakalar
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        //Sistemdeki ilgili validator'ları bulup getirir(DI sayesinde)
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(!_validators.Any()) //Bu request için validator var mı? yoksa devam et
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            //Tüm kuralları çalıştır
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            //Hata varsa "ValidationException" fırlat
            if(failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            return await next();
         }
    }
}
