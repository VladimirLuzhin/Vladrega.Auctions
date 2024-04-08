using FluentResults;
using MediatR;
using Vladrega.Auctions.Application.Notifications;

namespace Vladrega.Auctions.Application.Bets;

/// <summary>
/// Обработчик команды для добавления ставки на лот в ауцкионе
/// </summary>
public class DoBetCommandHandler : IRequestHandler<DoBetCommand, Result>
{
    private readonly UnitOfWork _unitOfWork;
    private INotificator _notificator;

    /// <summary>
    /// .ctor
    /// </summary>
    public DoBetCommandHandler(UnitOfWork unitOfWork, INotificator notificator)
    {
        _unitOfWork = unitOfWork;
        _notificator = notificator;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(DoBetCommand request, CancellationToken cancellationToken)
    {
        var auction = (await _unitOfWork.Auctions.GetCollectionAsync(cancellationToken))
            .FirstOrDefault(a => a.Id == request.AuctionId);

        if (auction is null)
            return Result.Fail("Нельзя обновить данный лот, т.к. для ауцкиона запрещено редактирование");

        var result = auction.DoBet(request.LotId, Guid.NewGuid());
        if (result.IsFailed)
            return Result.Fail(result.Errors);

        var lot = auction.Lots[request.LotId];

        await _notificator.NotifyAboutBetsAsync(lot.Id, lot.Bets, cancellationToken);
        // TODO сохранение ставки
        // await _unitOfWork.Bets.SaveAsync(new[] {result.Value}, cancellationToken);
        
        return Result.Ok();
    }
}