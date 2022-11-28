using FluentResults;
using Vladrega.Auctions.Application.Mediator;

namespace Vladrega.Auctions.Application.Lots.Delete;

/// <summary>
/// Валидатор команды удаления лота
/// </summary>
public class DeleteLotCommandValidator : IValidator<DeleteLotCommand>
{
    /// <inheritdoc />
    public Result Validate(DeleteLotCommand? request)
    {
        if (request?.LotId == Guid.Empty)
            return Result.Fail("Передан некорректный идентификатор лота");
        
        return Result.Ok();
    }
}