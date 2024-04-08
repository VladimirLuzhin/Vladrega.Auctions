using FluentResults;
using Vladrega.Auctions.Application.Mediator;

namespace Vladrega.Auctions.Application.Bets;

public class DoBetCommandValidator : IValidator<DoBetCommand>
{
    public Result Validate(DoBetCommand? request)
    {
        if (request?.LotId == Guid.Empty)
            return Result.Fail("Передан некорректный формат данных");
        
        return Result.Ok();
    }
}