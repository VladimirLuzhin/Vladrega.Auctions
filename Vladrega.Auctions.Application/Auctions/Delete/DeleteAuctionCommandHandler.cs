using FluentResults;
using MediatR;

namespace Vladrega.Auctions.Application.Auctions.Delete;

/// <summary>
/// Обработчик команды удаления ауцкиона
/// </summary>
public class DeleteAuctionCommandHandler : IRequestHandler<DeleteAuctionCommand, Result>
{
    private readonly UnitOfWork _unitOfWork;

    /// <summary>
    /// .ctor
    /// </summary>
    public DeleteAuctionCommandHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(DeleteAuctionCommand request, CancellationToken cancellationToken)
    {
        var allAuctions = await _unitOfWork.Auctions.GetAsync(cancellationToken);
        var existedAuction = allAuctions.FirstOrDefault(a => a.Id == request.AuctionId);

        if (existedAuction is null)
            return Result.Ok();
        
        if (!existedAuction.IsEditable)
            return Result.Fail("Данный ауцкион нельзя удалить");
        
        await _unitOfWork.Auctions.DeleteAsync(new[] { existedAuction }, cancellationToken);
        return Result.Ok();
    }
}