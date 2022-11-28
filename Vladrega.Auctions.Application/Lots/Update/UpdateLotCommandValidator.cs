using FluentResults;
using Vladrega.Auctions.Application.Mediator;

namespace Vladrega.Auctions.Application.Lots.Update;

/// <summary>
/// Валидатор команды обновления лота
/// </summary>
public class UpdateLotCommandValidator : IValidator<UpdateLotCommand>
{
    /// <inheritdoc />
    public Result Validate(UpdateLotCommand? request)
    {
        if (request is null)
            return Result.Fail("Не удалось распознать данные");
        
        if (request.LotId == Guid.Empty)
            return Result.Fail("Передан некорректный идентификатор лота");
        
        if (request.BetStep <= 0m)
            return Result.Fail("Шаг ставки не может быть меньше или равен нуля");
        
        if (request.BuyoutPrice <= 0m)
            return Result.Fail("Стоимость выкупа не может быть меньше или равна нуля");
        
        if (string.IsNullOrWhiteSpace(request.Code))
            return Result.Fail("Передан пустой код");
        
        if (string.IsNullOrWhiteSpace(request.Name))
            return Result.Fail("Передано пустое название лота");
        
        if (string.IsNullOrWhiteSpace(request.Description))
            return Result.Fail("Передано пустое описание лота");
        
        return Result.Ok();
    }
}