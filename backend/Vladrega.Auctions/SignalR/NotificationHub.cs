using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using Vladrega.Auctions.Application.Notifications;
using Vladrega.Auctions.Domain;

namespace Vladrega.Auctions.SignalR;

public class NotificationHub : Hub, INotificator
{
    private enum Methods
    {
        OnDoBet
    }

    private readonly ConcurrentDictionary<string, IClientProxy> _clients = new();

    public override Task OnConnectedAsync()
    {
        _clients.AddOrUpdate(Context.ConnectionId, Clients.Caller, (_, __) => Clients.Caller);
        return Task.CompletedTask;
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _clients.TryRemove(Context.ConnectionId, out _);
        return Task.CompletedTask;
    }

    public async Task NotifyAboutBetsAsync(Guid lotId, IEnumerable<Bet> bets, CancellationToken cancellationToken)
    {
        foreach (var client in _clients.Values)
        {
            await client.SendCoreAsync(Methods.OnDoBet.ToString(), new[] 
            {
                new 
                {
                    lotId = lotId,
                    bets = bets.Select(b => new
                    {
                        id = b.Id,
                        authoName = b.AuthorId,
                        amount = b.Amount,
                        date = b.DateTime
                    })
                }
            }, cancellationToken);
        }
    }
}