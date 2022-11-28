using FluentResults;
using MediatR;

namespace Vladrega.Auctions.Application.Lots.Delete;

/// <summary>
/// Обработчик команды удаления лота
/// </summary>
public class DeleteLotCommandHandler : IRequestHandler<DeleteLotCommand, Result>
{
    private readonly UnitOfWork _unitOfWork;

    /// <summary>
    /// .ctor
    /// </summary>
    public DeleteLotCommandHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(DeleteLotCommand request, CancellationToken cancellationToken)
    {
        var auction = (await _unitOfWork.Auctions
            .GetAsync(cancellationToken))
            .FirstOrDefault(a => a.Id == request.AuctionId);

        if (auction is null)
            return Result.Fail("Аукциона по указанному идентификатору не найден");

        var result = auction.RemoveLot(request.LotId);
        if (result.IsFailed)
            return Result.Fail(result.Errors);
        
        await _unitOfWork.Lots.DeleteAsync(new[] { result.Value }, cancellationToken);
        return Result.Ok();
    }
}