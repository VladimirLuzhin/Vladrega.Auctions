namespace Vladrega.Auctions.Domain;

/// <summary>
/// Статус лота
/// </summary>
public enum LotStatus
{
    /// <summary>
    /// Идут торги
    /// </summary>
    Bidding = 0,
    
    /// <summary>
    /// Торги завершены
    /// </summary>
    Complete = 1
}