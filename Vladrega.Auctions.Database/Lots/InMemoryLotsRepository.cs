using System.Collections.Concurrent;
using Vladrega.Auctions.Application;
using Vladrega.Auctions.Domain;

namespace Vladrega.Auctions.Database.Lots;

public class InMemoryLotsRepository : IRepository<Lot>
{
    private readonly ConcurrentDictionary<Guid, Lot> _lots = new();
    
    public Task SaveAsync(IEnumerable<Lot> objects, CancellationToken cancellationToken)
    {
        foreach (var lot in objects)
        {
            if (_lots.ContainsKey(lot.Id))
                continue;

            _lots.TryAdd(lot.Id, lot);
        }
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(IEnumerable<Lot> objects, CancellationToken cancellationToken)
    {
        foreach (var lot in objects)
        {
            _lots.TryRemove(lot.Id, out _);
        }
        
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<Lot>> GetAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult<IReadOnlyCollection<Lot>>(_lots.Values.ToArray());
    }
}