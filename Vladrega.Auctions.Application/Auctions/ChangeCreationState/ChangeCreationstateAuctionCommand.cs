using System.Text.Json.Serialization;
using FluentResults;
using MediatR;

namespace Vladrega.Auctions.Application.Auctions.ChangeCreationState;

/// <summary>
/// Команда для отмены аукциона
/// </summary>
public record ChangeAuctionCreationCommand : IRequest<Result>
{
    /// <summary>
    /// Идентификатор аукциона
    /// </summary>
    [JsonPropertyName("auctionId")]
    public Guid AuctionId { get; init; }
}