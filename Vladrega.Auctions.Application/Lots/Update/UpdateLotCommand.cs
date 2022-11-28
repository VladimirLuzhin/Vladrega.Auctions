using System.Text.Json.Serialization;
using FluentResults;
using MediatR;

namespace Vladrega.Auctions.Application.Lots.Update;

/// <summary>
/// Команда для обновления информации по лоту
/// </summary>
public record UpdateLotCommand : IRequest<Result>
{
    /// <summary>
    /// Идентификатор аукциона 
    /// </summary>
    [JsonPropertyName("auctionId")]
    public Guid AuctionId { get; init; }
    
    /// <summary>
    /// Идентификатор лота
    /// </summary>
    [JsonPropertyName("lotId")]
    public Guid LotId { get; init; }
    
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
    /// Шаг ставки лота
    /// </summary>
    [JsonPropertyName("betStep")]
    public decimal BetStep { get; init; }
    
    /// <summary>
    /// Стоимость выкупа у лота
    /// </summary>
    [JsonPropertyName("buyoutPrice")]
    public decimal? BuyoutPrice { get; init; }
}