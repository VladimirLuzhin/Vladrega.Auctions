using System.Text.Json.Serialization;
using FluentResults;
using MediatR;
using Vladrega.Auctions.Application.Auctions.Delete;

namespace Vladrega.Auctions.Application.Lots.Delete;

/// <summary>
/// Команда на удаление лота
/// </summary>
public record DeleteLotCommand : IRequest<Result>
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
}