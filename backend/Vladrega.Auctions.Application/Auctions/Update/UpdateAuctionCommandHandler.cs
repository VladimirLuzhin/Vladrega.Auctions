using FluentResults;
using MediatR;

namespace Vladrega.Auctions.Application.Auctions.Update;

/// <summary>
/// Обработчик команды обновления ауцкиона
/// </summary>
public class UpdateAuctionCommandHandler : IRequestHandler<UpdateAuctionCommand, Result>
{
    private readonly UnitOfWork _unitOfWork;

    /// <summary>
    /// .ctor
    /// </summary>
    public UpdateAuctionCommandHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(UpdateAuctionCommand request, CancellationToken cancellationToken)
    {
        // TODO specification
        var auction = (await _unitOfWork.Auctions.GetCollectionAsync(cancellationToken))
            .FirstOrDefault(a => a.Id == request.AuctionId);
        
        if (auction is null)
            return Result.Fail("Аукцион с переданным идентификатором не существует");

        var result = auction.UpdateInformation(request.Name, request.DateStart, request.DateEnd);
        if (result.IsFailed)
            return Result.Fail(result.Errors);
        
        await _unitOfWork.Auctions.SaveAsync(new[] { auction }, cancellationToken);
        
        return Result.Ok();
    }
}