namespace Vladrega.Auctions.Domain;

/// <summary>
/// Аукцион
/// </summary>
public class Auction
{
    /// <summary>
    /// Идентификатор аукциона
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Название аукциона
    /// </summary>
    public string Name { get; init; }
    
    /// <summary>
    /// Автор аукциона
    /// </summary>
    public int AuthorId { get; init; }
    
    /// <summary>
    /// Дата начала аукциона
    /// </summary>
    public DateTime DateStart { get; init; }

    /// <summary>
    /// Флаг, оформляется ли аукциона сейчас
    /// </summary>
    public bool IsCreation { get; init; }
    
    private DateTime _dateEnd;

    /// <summary>
    /// Дата завершения аукциона
    /// </summary>
    public DateTime DateEnd
    {
        get
        {
            // Логика автопродления аукциона. Если ставка сделана за 30 или менее секунд до конца аукциона, продлеваем его на 30 секунд,
            // воизбежание ситуаций, когда пользователи будут пытаться сделать ставку под конец аукциона.
            var maxBetDate = Lots.Values.SelectMany(l => l.Bets).Max(s => s.DateTime).AddSeconds(30);
            return _dateEnd > maxBetDate ? _dateEnd : maxBetDate;
        }
        init => _dateEnd = value;
    }

    /// <summary>
    /// Статус аукциона
    /// </summary>
    public AuctionStatus Status
    {
        get
        {
            if (IsCreation)
                return AuctionStatus.Creation;

            var dateTimeNow = DateTime.UtcNow;
            if (dateTimeNow > DateStart && dateTimeNow < DateEnd)
                return AuctionStatus.Bidding;
            
            if (dateTimeNow < DateStart)
                return AuctionStatus.WaitBidding;

            return AuctionStatus.Complete;
        }
    }

    /// <summary>
    /// Лоты по аукциону
    /// </summary>
    public Dictionary<int, Lot> Lots { get; init; } = new();
}