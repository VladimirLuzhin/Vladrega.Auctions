using System.Text.Json.Serialization;
using FluentResults;
using MediatR;

namespace Vladrega.Auctions.Application.Auctions.Update;

/// <summary>
/// Команда для обновления данных аукциона
/// </summary>
public record UpdateAuctionCommand : IRequest<Result>
{
    /// <summary>
    /// Идентификатор аукциона
    /// </summary>
    [JsonPropertyName("auctionId")]
    public Guid AuctionId { get; init; }
    
    /// <summary>
    /// Новое название ауцкиона
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }
    
    /// <summary>
    /// Новая дата старта аукциона
    /// </summary>
    [JsonPropertyName("dateStart")]
    public DateTime DateStart { get; init; }
    
    /// <summary>
    /// Новая дата завершения аукциона
    /// </summary>
    [JsonPropertyName("dateEnd")]
    public DateTime DateEnd { get; init; }
}