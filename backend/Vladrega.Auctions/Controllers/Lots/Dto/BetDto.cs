using System.Text.Json.Serialization;

namespace Vladrega.Auctions.Controllers.Lots;

/// <summary>
/// DTO ставки
/// </summary>
public class BetDto
{
    /// <summary>
    /// Идентификатор ставки
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; init; }
    
    /// <summary>
    /// Пользователь, слелавший ставку
    /// </summary>
    [JsonPropertyName("authorName")]
    public string AuthorName { get; init; }
    
    /// <summary>
    /// Размер ставки
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal Amount { get; init; }
    
    /// <summary>
    /// Дата ставки
    /// </summary>
    [JsonPropertyName("date")]
    public DateTime DateTime { get; init; }
}