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
    /// .ctor
    /// </summary>
    public UnitOfWork(IRepository<Auction> auctions)
    {
        Auctions = auctions;
    }
}