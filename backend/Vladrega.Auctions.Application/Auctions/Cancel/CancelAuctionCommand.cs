using System.Text.Json.Serialization;
using FluentResults;
using MediatR;

namespace Vladrega.Auctions.Application.Auctions.Cancel;

/// <summary>
/// Команда для отмены аукциона
/// </summary>
public record CancelAuctionCommand : IRequest<Result>
{
    /// <summary>
    /// Идентификатор аукциона
    /// </summary>
    [JsonPropertyName("auctionId")]
    public Guid AuctionId { get; init; }
}