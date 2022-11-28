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
        var lots = await _unitOfWork.Lots
            .GetAsync(cancellationToken);
        
        return Result.Ok<IEnumerable<Lot>>(lots);
    }
}