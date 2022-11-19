using FluentResults;
using MediatR;
using Vladrega.Auctions.Application.Mediator;

namespace Vladrega.Auctions.Application.Behaviours;

/// <summary>
/// Пайплайн валидации входящих команд
/// </summary>
public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TResponse : ResultBase, new() 
    where TRequest : IRequest<TResponse>
{
    private readonly IValidator<TRequest> _validator;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="validator">Валидатор конкретной команды \ запроса</param>
    public ValidationBehaviour(IValidator<TRequest> validator)
    {
        _validator = validator;
    }
    
    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);
        if (validationResult.IsFailed)
        {
            var result = new TResponse();
            result.Reasons.AddRange(validationResult.Reasons);
            return result;
        }

        return await next();
    }
}