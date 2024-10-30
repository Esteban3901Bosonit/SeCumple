using FluentValidation;
using MediatR;

namespace SeCumple.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any()) return await next();
        var context = new ValidationContext<TRequest>(request);
        var validationResults =
            await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));
        var failures = validationResults.SelectMany(x => x.Errors).Where(y => y != null).ToList();
        if (failures.Count > 0)
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}