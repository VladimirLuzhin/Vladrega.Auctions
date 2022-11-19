using FluentResults;
using Vladrega.Auctions.Application.Mediator;

namespace Vladrega.Auctions.Application.Auctions.CreateAuction;

/// <summary>
/// Валидатор команды создания аукциона
/// </summary>
public class CreateActionCommandValidator : IValidator<CreateActionCommand>
{
    /// <inheritdoc />
    public Result Validate(CreateActionCommand? request)
    {
        if (request is null)
            return Result.Fail("Не удалось распознать данные");
        
        if (string.IsNullOrWhiteSpace(request.Name))
            return Result.Fail("Передано пустое имя");
        
        if (request.DateEnd == default)
            return Result.Fail("Передана некорректная дата завершения аукциона");
        
        if (request.DateStart == default)
            return Result.Fail("Передана некорректная дата начала");
        
        if (request.DateEnd <= request.DateStart)
            return Result.Fail("Дата завершения не может быть меньше или равна даты начала");
        
        return Result.Ok();
    }
}