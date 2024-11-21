using FluentValidation;
using FluentValidation.Results;
using HootelBooking.Application.Models;
using MediatR;


namespace HootelBooking.Application.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

        var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(request, cancellationToken)));
        var failures = validationResults.SelectMany(result => result.Errors).Where(f => f != null).ToList();

        // If there are validation failures, throw an exception
        if (failures.Any())
        {
            throw new FluentValidation.ValidationException(failures);
        }

        return await next();


           
        }
    }
}
