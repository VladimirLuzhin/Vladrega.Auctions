using FluentResults;
using Vladrega.Auctions.Application.Mediator;

namespace Vladrega.Auctions.Application.Auctions.Delete;

/// <inheritdoc />
public class DeleteAuctionCommandValidator : IValidator<DeleteAuctionCommand>
{
    /// <inheritdoc />
    public Result Validate(DeleteAuctionCommand? request)
    {
        if (request is null)
            return Result.Fail("Не удалось распознать данные");

        if (request.AuctionId == Guid.Empty)
            return Result.Fail("Передан некорректный идентификатор");
        
        return Result.Ok();
    }
}