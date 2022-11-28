using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vladrega.Auctions.Application.Auctions.Cancel;
using Vladrega.Auctions.Application.Auctions.ChangeCreationState;
using Vladrega.Auctions.Application.Auctions.Create;
using Vladrega.Auctions.Application.Auctions.Delete;
using Vladrega.Auctions.Application.Auctions.Get;
using Vladrega.Auctions.Application.Auctions.Update;
using Vladrega.Auctions.Controllers.Auction.Dto;

namespace Vladrega.Auctions.Controllers.Auction;

/// <summary>
/// Контроллер для работы с аукционом
/// </summary>
[Route("api/v1/auctions")]
public class AuctionController : BaseController
{
    private readonly IMediator _mediator;

    /// <summary>
    /// .ctor
    /// </summary>
    public AuctionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создание аукциона
    /// </summary>
    /// <param name="command">Команда для создания аукциона</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpPost]
    public async Task<IActionResult> CreateAuctionAsync(CreateAuctionCommand command, CancellationToken cancellationToken)
    {
        return ConvertToActionResult(await _mediator.Send(command, cancellationToken));
    }

    /// <summary>
    /// Отмена аукциона
    /// </summary>
    /// <param name="command">Команда для отмены аукциона</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpPost("cancel")]
    public async Task<IActionResult> CancelAuctionAsync(CancelAuctionCommand command, CancellationToken cancellationToken)
    {
        return ConvertToActionResult(await _mediator.Send(command, cancellationToken));
    }
    
    /// <summary>
    /// Изменение состояния оформления аукциона
    /// </summary>
    /// <param name="command">Команда для изменения состояния оформления аукциона</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpPost("changeCreationState")]
    public async Task<IActionResult> CompleteCreationAsync(ChangeAuctionCreationCommand command, CancellationToken cancellationToken)
    {
        return ConvertToActionResult(await _mediator.Send(command, cancellationToken));
    }

    /// <summary>
    /// Удаление аукциона
    /// </summary>
    /// <param name="command">Команда для удаления аукциона</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpDelete]
    public async Task<IActionResult> DeleteAuctionAsync(DeleteAuctionCommand command, CancellationToken cancellationToken)
    {
        return ConvertToActionResult(await _mediator.Send(command, cancellationToken));
    }

    /// <summary>
    /// Обновление данных по аукциону
    /// </summary>
    /// <param name="command">Команда для обновления данных аукциона</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpPut]
    public async Task<IActionResult> UpdateAuctionAsync(UpdateAuctionCommand command, CancellationToken cancellationToken)
    {
        return ConvertToActionResult(await _mediator.Send(command, cancellationToken));
    }

    /// <summary>
    /// Получение аукционов
    /// </summary>
    /// <param name="query">Запрос на получение аукционов</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpGet]
    public async Task<IActionResult> GetAuctionsAsync([FromQuery] GetAuctionsQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        // TODO общий метод с учетом Generic-оф
        if (result.IsFailed)
            return BadRequest(string.Join(", ", result.Reasons.Select(r => r.Message)));

        // TODO AutoMapper / Mapster
        var auctionDtos = result.Value.Select(a => new AuctionDto
        {
            Id = a.Id,
            Name = a.Name,
            Status = a.Status,
            DateStart = a.DateStart,
            DateEnd = a.DateEnd
        });
        
        return Ok(auctionDtos);
    }
}