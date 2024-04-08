using System.Text.Json.Serialization;
using Vladrega.Auctions.Domain;

namespace Vladrega.Auctions.Controllers.Lots;

/// <summary>
/// DTO лота
/// </summary>
public record LotDto
{
    /// <summary>
    /// Идентификатор лота
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; init; }
    
    /// <summary>
    /// Название лота
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }
    
    /// <summary>
    /// Код лота
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; init; }
    
    /// <summary>
    /// Описание лота
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; init; }

    /// <summary>
    /// Шаг ставки
    /// </summary>
    [JsonPropertyName("betStep")]
    public decimal BetStep { get; init; }
    
    /// <summary>
    /// Стоимость выкупа
    /// </summary>
    [JsonPropertyName("buyoutPrice")]
    public decimal? BuyoutPrice { get; init; }
    
    /// <summary>
    /// Ставки по лоту
    /// </summary>
    [JsonPropertyName("bets")]
    public IEnumerable<BetDto> Bets { get; init; }

    /// <summary>
    /// Картинки лота
    /// </summary>
    [JsonPropertyName("images")]
    public IReadOnlyCollection<string> Images { get; init; }

    /// <summary>
    /// Выкуплен ли лот
    /// </summary>
    [JsonPropertyName("isPurchased")]
    public bool IsPurchased { get; init; }
}