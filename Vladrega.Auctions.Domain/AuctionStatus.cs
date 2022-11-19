namespace Vladrega.Auctions.Domain;

/// <summary>
/// Статус акциона
/// </summary>
public enum AuctionStatus
{
    /// <summary>
    /// Неизвестный статус
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// Этап создания аукциона
    /// </summary>
    Creation = 1,
    
    /// <summary>
    /// Этап ожидания торгов
    /// </summary>
    WaitBidding = 2,
    
    /// <summary>
    /// Этап торгов
    /// </summary>
    Bidding = 3,
    
    /// <summary>
    /// Аукцион завершен
    /// </summary>
    Complete = 4
}