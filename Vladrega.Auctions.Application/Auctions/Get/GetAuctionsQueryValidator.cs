using FluentResults;
using Vladrega.Auctions.Application.Mediator;

namespace Vladrega.Auctions.Application.Auctions.Get;

/// <inheritdoc />
public class GetAuctionsQueryValidator : IValidator<GetAuctionsQuery>
{
    /// <inheritdoc />
    public Result Validate(GetAuctionsQuery? request)
    {
        if (request is null)
            return Result.Fail("Не удалось распознать данные");
        
        if (request.LastAuctionId.HasValue && request.LastAuctionId.Value <= 0)
            return Result.Fail("Передан некорректный идентификатор последнего ауцкиона");

        return Result.Ok();
    }
}