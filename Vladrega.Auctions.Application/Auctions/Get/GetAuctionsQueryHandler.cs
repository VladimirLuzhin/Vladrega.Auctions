using FluentResults;
using MediatR;
using Vladrega.Auctions.Domain;

namespace Vladrega.Auctions.Application.Auctions.Get;

/// <summary>
/// Обработчик запроса на получение списка ауцкионов
/// </summary>
public class GetAuctionsQueryHandler : IRequestHandler<GetAuctionsQuery, Result<IEnumerable<Auction>>>
{
    private readonly UnitOfWork _unitOfWork;

    /// <summary>
    /// .ctor
    /// </summary>
    public GetAuctionsQueryHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<Result<IEnumerable<Auction>>> Handle(GetAuctionsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Auction> auctions;

        if (request.LastAuctionId.HasValue)
            auctions = (await _unitOfWork.Auctions.GetAsync(cancellationToken))
                .Take(50);
        else
            auctions = (await _unitOfWork.Auctions.GetAsync(cancellationToken))
                .Take(50);
        
        return Result.Ok(auctions);
    }
}