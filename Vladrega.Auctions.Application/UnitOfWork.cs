using Vladrega.Auctions.Domain;

namespace Vladrega.Auctions.Application;

/// <summary>
/// Объект для работы с получением, сохранением, обновлением и удалением всех данных. Так же фиксирует и коммитит изменения
/// </summary>
public class UnitOfWork
{
    /// <summary>
    /// Репозиторий для работы с аукционами
    /// </summary>
    public IRepository<Auction> Auctions { get; }
    
    /// <summary>
    /// Репозиторий для работы с лотами
    /// </summary>
    public IRepository<Lot> Lots { get; }
    
    /// <summary>
    /// Репозиторий для работы со ставками
    /// </summary>
    public IRepository<Bet> Bets { get; }

    /// <summary>
    /// .ctor
    /// </summary>
    public UnitOfWork(IRepository<Auction> auctions, IRepository<Lot> lots, IRepository<Bet> bets)
    {
        Auctions = auctions;
        Lots = lots;
        Bets = bets;
    }
}