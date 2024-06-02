using FluentResults;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Application.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse> where TResponse : IResultBase
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v =>
                v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .Select(c => new Error(c.ErrorMessage))
            .ToArray();

        if (failures.Any())
            return GetResult<TResponse>(failures);

        return await next();
    }

    private TResult GetResult<TResult>(Error[] failures) where TResult : IResultBase
    {
        if (typeof(TResult) == typeof(Result))
            return (TResult)(IResultBase)Result.Fail(failures);

        var makeme = typeof(Result<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0]);

        object o = Activator.CreateInstance(makeme);

        var result = makeme
            .GetMethod(nameof(Result.WithReasons))!
            .Invoke(o, new object?[] { failures })!;

        return (TResult)result;
    }
}