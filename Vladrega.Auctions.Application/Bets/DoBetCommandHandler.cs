using FluentResults;
using MediatR;

namespace Vladrega.Auctions.Application.Bets;

/// <summary>
/// Обработчик команды для добавления ставки на лот в ауцкионе
/// </summary>
public class DoBetCommandHandler : IRequestHandler<DoBetCommand, Result>
{
    private readonly UnitOfWork _unitOfWork;

    /// <summary>
    /// .ctor
    /// </summary>
    public DoBetCommandHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(DoBetCommand request, CancellationToken cancellationToken)
    {
        var auction = (await _unitOfWork.Auctions.GetAsync(cancellationToken))
            .FirstOrDefault(a => a.Id == request.AuctionId);

        if (auction is null)
            return Result.Fail("Нельзя обновить данный лот, т.к. для ауцкиона запрещено редактирование");

        var result = auction.DoBet(request.LotId, Guid.NewGuid());
        if (result.IsFailed)
            return Result.Fail(result.Errors);

        await _unitOfWork.Bets.SaveAsync(new[] {result.Value}, cancellationToken);
        return Result.Ok();
    }
}