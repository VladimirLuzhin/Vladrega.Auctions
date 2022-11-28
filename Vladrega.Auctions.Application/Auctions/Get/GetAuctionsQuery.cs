using System.Text.Json.Serialization;
using FluentResults;
using MediatR;
using Vladrega.Auctions.Domain;

namespace Vladrega.Auctions.Application.Auctions.Get;

/// <summary>
/// Запрос на получение аукционов
/// </summary>
public class GetAuctionsQuery : IRequest<Result<IEnumerable<Auction>>>
{
    /// <summary>
    /// Идентификатор последнего аукциона (для пагинации)
    /// </summary>
    [JsonPropertyName("auctionId")]
    public int? LastAuctionId { get; init; }
}