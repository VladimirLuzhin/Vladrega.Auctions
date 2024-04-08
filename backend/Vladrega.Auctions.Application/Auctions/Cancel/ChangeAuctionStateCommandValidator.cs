using FluentResults;
using Vladrega.Auctions.Application.Mediator;

namespace Vladrega.Auctions.Application.Auctions.Cancel;

/// <inheritdoc />
public class CancelAuctionCommandValidator : IValidator<CancelAuctionCommand>
{
    /// <inheritdoc />
    public Result Validate(CancelAuctionCommand? request)
    {
        if (request is null)
            return Result.Fail("Не удалось распознать данные");

        if (request.AuctionId == Guid.Empty)
            return Result.Fail("Передан некорректный идентификатор");
        
        return Result.Ok();
    }
}