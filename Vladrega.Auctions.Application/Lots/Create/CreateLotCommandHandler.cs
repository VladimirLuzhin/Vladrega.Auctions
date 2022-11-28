using FluentResults;
using MediatR;
using Vladrega.Auctions.Domain;

namespace Vladrega.Auctions.Application.Lots.Create;

/// <summary>
/// Обработчик команды создания лота
/// </summary>
public class CreateLotCommandHandler : IRequestHandler<CreateLotCommand, Result>
{
    private readonly UnitOfWork _unitOfWork;

    /// <summary>
    /// .ctor
    /// </summary>
    public CreateLotCommandHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(CreateLotCommand request, CancellationToken cancellationToken)
    {
        var auction = (await _unitOfWork.Auctions
            .GetAsync(cancellationToken))
            .FirstOrDefault(a => a.Id == request.AuctionId);
        
        if (auction is null)
            return Result.Fail("Аукцион с переданным идентификатором не найден");

        var result = auction.AddLot(request.Name, request.Code, request.Description, request.BetStep, request.BuyoutPrice);
        if (result.IsFailed)
            return Result.Fail(result.Errors);
        
        await _unitOfWork.Lots.SaveAsync(new [] { result.Value }, cancellationToken);
        return Result.Ok();
    }
}