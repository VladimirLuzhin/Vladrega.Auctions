using System.Collections.Concurrent;
using Vladrega.Auctions.Application;
using Vladrega.Auctions.Domain;

namespace Vladrega.Auctions.Database.Auctions;

/// <summary>
/// InMemory реализация
/// </summary>
public class InMemoryAuctionsRepository : IRepository<Auction>
{
    private readonly ConcurrentDictionary<Guid, Auction> _auctions;

    public InMemoryAuctionsRepository()
    {
        var auctions = new List<Auction>
        {
            CreateAuctions(),
            CreateAuctions(),
            CreateAuctions(),
            CreateAuctions(),
            CreateAuctions(),
            CreateAuctions(),
            CreateAuctions(),
            CreateAuctions(),
            CreateAuctions(),
            CreateAuctions(),
            CreateAuctions(),
            CreateAuctions()
        };

        _auctions = new ConcurrentDictionary<Guid, Auction>
        (
            auctions.ToDictionary(a => a.Id, a => a)
        );

        Auction CreateAuctions()
        {
            var aucionWithLots = new Auction("Аукцион с супер длинным названием азasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdазазаз", Guid.NewGuid(),
                DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1));
        
            if (DateTime.UtcNow.Ticks % 2 == 0)
                aucionWithLots.AddLot("Название лота 1", "123123", "Описание", 100, 1000, "https://c.dns-shop.ru/thumb/st4/fit/320/250/ea947e2621a7a1ace0635912e5de4249/3dfaf5268b34aa2398a14e8906fb3d7720f5f945551ccd244419f29363db48f1.jpg");
            else
                aucionWithLots.AddLot("Название лота 1", "123123", "Описание", 100, 1000, "https://clips-media-assets2.twitch.tv/i1dlSa2UaNcRmeCVdIoihw/AT-cm%7Ci1dlSa2UaNcRmeCVdIoihw-preview-480x272.jpg");
            
            aucionWithLots.AddLot("Название лота 2", "123123", "Описание", 100, null, "https://c.dns-shop.ru/thumb/st4/fit/320/250/ea947e2621a7a1ace0635912e5de4249/3dfaf5268b34aa2398a14e8906fb3d7720f5f945551ccd244419f29363db48f1.jpg");
            aucionWithLots.AddLot("Название лота 3", "123123", "Описание", 100, 1000, "https://c.dns-shop.ru/thumb/st4/fit/320/250/ea947e2621a7a1ace0635912e5de4249/3dfaf5268b34aa2398a14e8906fb3d7720f5f945551ccd244419f29363db48f1.jpg");
            aucionWithLots.AddLot("Название лота 4", "123123", "Описание", 100, 1000, "https://c.dns-shop.ru/thumb/st4/fit/320/250/ea947e2621a7a1ace0635912e5de4249/3dfaf5268b34aa2398a14e8906fb3d7720f5f945551ccd244419f29363db48f1.jpg");
            aucionWithLots.AddLot("Название лота 5", "123123", "Описание", 100, 1000, "https://c.dns-shop.ru/thumb/st4/fit/320/250/ea947e2621a7a1ace0635912e5de4249/3dfaf5268b34aa2398a14e8906fb3d7720f5f945551ccd244419f29363db48f1.jpg");
            aucionWithLots.AddLot("Название лота 6", "123123", "Описание", 100, 1000, "https://c.dns-shop.ru/thumb/st4/fit/320/250/ea947e2621a7a1ace0635912e5de4249/3dfaf5268b34aa2398a14e8906fb3d7720f5f945551ccd244419f29363db48f1.jpg");
            aucionWithLots.AddLot("Название лота 7", "123123", "Описание", 100, 1000, "https://c.dns-shop.ru/thumb/st4/fit/320/250/ea947e2621a7a1ace0635912e5de4249/3dfaf5268b34aa2398a14e8906fb3d7720f5f945551ccd244419f29363db48f1.jpg");
            aucionWithLots.AddLot("Название лота 8", "123123", "Описание", 100, 1000, "https://c.dns-shop.ru/thumb/st4/fit/320/250/ea947e2621a7a1ace0635912e5de4249/3dfaf5268b34aa2398a14e8906fb3d7720f5f945551ccd244419f29363db48f1.jpg");
        
            aucionWithLots.ChangeCreationState();

            return aucionWithLots;
        }
    }
    
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
    public Task<IReadOnlyCollection<Auction>> GetCollectionAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult<IReadOnlyCollection<Auction>>(_auctions.Values.ToArray());
    }
}