namespace Vladrega.Auctions.Domain;

/// <summary>
/// Ставка
/// </summary>
public class Bet
{
    /// <summary>
    /// Идентификатор ставки
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Пользователь, слелавший ставку
    /// </summary>
    public Guid AuthorId { get; init; }
    
    /// <summary>
    /// Идентификатор лота, по которому совершена ставка
    /// </summary>
    public Guid LotId { get; init; }
    
    /// <summary>
    /// Размер ставки
    /// </summary>
    public decimal Amount { get; init; }
    
    /// <summary>
    /// Дата ставки
    /// </summary>
    public DateTime DateTime { get; init; }
}