using FluentResults;
using MediatR;

namespace Vladrega.Auctions.Application.Auctions.Cancel;

/// <summary>
/// Обработчик команды отмены аукциона
/// </summary>
public class CancelAuctionCommandHandler : IRequestHandler<CancelAuctionCommand, Result>
{
    private readonly UnitOfWork _unitOfWork;

    /// <summary>
    /// .ctor
    /// </summary>
    public CancelAuctionCommandHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(CancelAuctionCommand request, CancellationToken cancellationToken)
    {
        // TODO specification
        var auction = (await _unitOfWork.Auctions.GetCollectionAsync(cancellationToken))
            .FirstOrDefault(a => a.Id == request.AuctionId);
        
        if (auction is null)
            return Result.Fail("Аукцион с переданным идентификатором не существует");
        
        var result = auction.Cancel();
        if (result.IsFailed)
            return result;
        
        await _unitOfWork.Auctions.SaveAsync(new [] { auction }, cancellationToken);
        return Result.Ok();
    }
}