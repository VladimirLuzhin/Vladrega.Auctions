using System.Collections.Concurrent;
using Vladrega.Auctions.Application;
using Vladrega.Auctions.Domain;

namespace Vladrega.Auctions.Database.Auctions;

/// <summary>
/// InMemory реализация
/// </summary>
public class InMemoryAuctionsRepository : IRepository<Auction>
{
    private readonly ConcurrentDictionary<Guid, Auction> _auctions = new();

    /// <inheritdoc />
    public Task SaveAsync(IEnumerable<Auction> objects, CancellationToken cancellationToken)
    {
        foreach (var auction in objects)
        {
            if (_auctions.TryGetValue(auction.Id, out _)) 
                continue;
            
            _auctions.TryAdd(auction.Id, auction);
        }

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task DeleteAsync(IEnumerable<Auction> objects, CancellationToken cancellationToken)
    {
        foreach (var auction in objects)
        {
            _auctions.TryRemove(auction.Id, out _);
        }
        
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task<IReadOnlyCollection<Auction>> GetAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult<IReadOnlyCollection<Auction>>(_auctions.Values.ToArray());
    }
}