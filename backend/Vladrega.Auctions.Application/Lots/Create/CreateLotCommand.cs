using System.Text.Json.Serialization;
using FluentResults;
using MediatR;

namespace Vladrega.Auctions.Application.Lots.Create;

/// <summary>
/// Команда для создания лота
/// </summary>
public record CreateLotCommand : IRequest<Result>
{
    /// <summary>
    /// Идентификатор ауцкиона для которого создаем лот
    /// </summary>
    [JsonPropertyName("auctionId")]
    public Guid AuctionId { get; init; }
    
    /// <summary>
    /// Название лота
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }
    
    /// <summary>
    /// Описание лота
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; init; }
    
    /// <summary>
    /// Код лота
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; init; }
    
    /// <summary>
    /// Шаг ставки
    /// </summary>
    [JsonPropertyName("betStep")]
    public decimal BetStep { get; init; }
    
    /// <summary>
    /// Стоимость выкупа лота
    /// </summary>
    [JsonPropertyName("buyoutPrice")]
    public decimal? BuyoutPrice { get; init; }
}