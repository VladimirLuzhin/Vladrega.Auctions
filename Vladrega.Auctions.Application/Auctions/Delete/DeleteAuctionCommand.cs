using System.Text.Json.Serialization;
using FluentResults;
using MediatR;

namespace Vladrega.Auctions.Application.Auctions.Delete;

/// <summary>
/// Команда для удаления ауцкиона
/// </summary>
public record DeleteAuctionCommand : IRequest<Result>
{
    /// <summary>
    /// Идентификатор аукциона
    /// </summary>
    [JsonPropertyName("auctionId")]
    public Guid AuctionId { get; init; }
}