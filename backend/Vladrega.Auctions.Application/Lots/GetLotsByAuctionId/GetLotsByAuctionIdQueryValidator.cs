using FluentResults;
using Vladrega.Auctions.Application.Mediator;

namespace Vladrega.Auctions.Application.Lots.GetLotsByAuctionId;

/// <summary>
/// Валидатор команды удаления лота
/// </summary>
public class GetLotsByAuctionIdQueryValidator : IValidator<GetLotsByAuctionIdQuery>
{
    /// <inheritdoc />
    public Result Validate(GetLotsByAuctionIdQuery? request)
    {
        if (request?.AuctionId == Guid.Empty)
            return Result.Fail("Передан некорректный идентификатор ауцкиона");
        
        return Result.Ok();
    }
}