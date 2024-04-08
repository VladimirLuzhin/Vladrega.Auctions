using System.Text.Json.Serialization;
using FluentResults;
using MediatR;
using Vladrega.Auctions.Domain;

namespace Vladrega.Auctions.Application.Lots.GetLotsByAuctionId;

/// <summary>
/// Запрос на получение лотов по идентификатору ауцкиона
/// </summary>
public record GetLotsByAuctionIdQuery : IRequest<Result<IEnumerable<Lot>>>
{
    /// <summary>
    /// Идентификатор ауцкиона
    /// </summary>
    [JsonPropertyName("auctionId")]
    public Guid AuctionId { get; init; }
}