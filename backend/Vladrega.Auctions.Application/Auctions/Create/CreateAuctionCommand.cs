using System.Text.Json.Serialization;
using FluentResults;
using MediatR;

namespace Vladrega.Auctions.Application.Auctions.Create;

/// <summary>
/// Команда на создание аукциона
/// </summary>
public record CreateAuctionCommand : IRequest<Result>
{
    /// <summary>
    /// Название аукциона
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }
    
    /// <summary>
    /// Дата начала
    /// </summary>
    [JsonPropertyName("dateStart")]
    public DateTime DateStart { get; init; }
    
    /// <summary>
    /// Дата завершения
    /// </summary>
    [JsonPropertyName("dateEnd")]
    public DateTime DateEnd { get; init; }
}