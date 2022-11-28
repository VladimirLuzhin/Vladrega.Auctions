using System.Collections.Concurrent;
using Vladrega.Auctions.Application;
using Vladrega.Auctions.Domain;

namespace Vladrega.Auctions.Database.Bets;

public class InMemoryBetsRepository : IRepository<Bet>
{
    private readonly ConcurrentDictionary<Guid, Bet> _bets = new();

    /// <inheritdoc />
    public Task SaveAsync(IEnumerable<Bet> objects, CancellationToken cancellationToken)
    {
        foreach (var bet in objects)
        {
            if (_bets.TryGetValue(bet.Id, out _)) 
                continue;
            
            _bets.TryAdd(bet.Id, bet);
        }

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task DeleteAsync(IEnumerable<Bet> objects, CancellationToken cancellationToken)
    {
        foreach (var bet in objects)
        {
            _bets.TryRemove(bet.Id, out _);
        }
        
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task<IReadOnlyCollection<Bet>> GetAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult<IReadOnlyCollection<Bet>>(_bets.Values.ToArray());
    }
}