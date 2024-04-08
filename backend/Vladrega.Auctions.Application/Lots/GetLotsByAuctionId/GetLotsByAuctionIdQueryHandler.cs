using FluentResults;
using MediatR;
using Vladrega.Auctions.Domain;

namespace Vladrega.Auctions.Application.Lots.GetLotsByAuctionId;

/// <summary>
/// Обработчик запроса на получение лота по идентификатору аукциона
/// </summary>
public class GetLotsByAuctionIdQueryHandler : IRequestHandler<GetLotsByAuctionIdQuery, Result<IEnumerable<Lot>>>
{
    private readonly UnitOfWork _unitOfWork;

    /// <summary>
    /// .ctor
    /// </summary>
    public GetLotsByAuctionIdQueryHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<Result<IEnumerable<Lot>>> Handle(GetLotsByAuctionIdQuery request, CancellationToken cancellationToken)
    {
        // TODO
        var auctions = await _unitOfWork.Auctions
            .GetCollectionAsync(cancellationToken);

        var filteredAuctions = auctions
            .FirstOrDefault(a => a.Id == request.AuctionId);

        var lots = filteredAuctions is null
            ? Enumerable.Empty<Lot>()
            : filteredAuctions.Lots.Values;
        
        return Result.Ok(lots);
    }
}