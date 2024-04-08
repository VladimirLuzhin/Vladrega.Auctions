using Vladrega.Auctions.Domain;

namespace Vladrega.Auctions.Application.Notifications;

public interface INotificator
{
    Task NotifyAboutBetsAsync(Guid lotId, IEnumerable<Bet> bets, CancellationToken cancellationToken);
}