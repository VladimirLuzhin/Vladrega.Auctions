using FluentResults;
using MediatR;

namespace Vladrega.Auctions.Application.Mediator;

/// <summary>
/// Интрефейс для объявления валидатора для использования в <see cref="ValidationBehaviour"/>
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IValidator<in T> where T : IBaseRequest
{
    /// <summary>
    /// Валидация команды \ запроса
    /// </summary>
    /// <param name="request">Команда \ запроса</param>
    /// <returns>Результат валидации</returns>
    Result Validate(T request);
}