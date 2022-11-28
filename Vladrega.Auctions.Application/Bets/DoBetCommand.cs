using System.Text.Json.Serialization;
using FluentResults;
using MediatR;

namespace Vladrega.Auctions.Application.Bets;

/// <summary>
/// Команда, чтобы сделать ставку на лот
/// </summary>
public record DoBetCommand : IRequest<Result>
{
    /// <summary>
    /// Идентификатор лота
    /// </summary>
    [JsonPropertyName("lotId")]
    public Guid LotId { get; init; }

    /// <summary>
    /// Идентификатор аукциона
    /// </summary>
    [JsonPropertyName("auctionId")]
    public Guid AuctionId { get; set; }
}