using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vladrega.Auctions.Application.Lots.Create;
using Vladrega.Auctions.Application.Lots.Delete;
using Vladrega.Auctions.Application.Lots.GetLotsByAuctionId;
using Vladrega.Auctions.Application.Lots.Update;

namespace Vladrega.Auctions.Controllers.Lots;

/// <summary>
/// Контроллер для работы с лотами
/// </summary>
[Route("api/v1/auctions/lots")]
public class LotsControllers : BaseController
{
    private readonly IMediator _mediator;

    /// <summary>
    /// .ctor
    /// </summary>
    public LotsControllers(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создать лот
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateLotAsync(CreateLotCommand command, CancellationToken cancellationToken)
    {
        return ConvertToActionResult(await _mediator.Send(command, cancellationToken));
    }

    /// <summary>
    /// Удалить лот
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> DeleteLotAsync([FromQuery] DeleteLotCommand command, CancellationToken cancellationToken)
    {
        return ConvertToActionResult(await _mediator.Send(command, cancellationToken));
    }

    /// <summary>
    /// Обновить лот
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> UpdateLotAsync(UpdateLotCommand command, CancellationToken cancellationToken)
    {
        return ConvertToActionResult(await _mediator.Send(command, cancellationToken));
    }

    /// <summary>
    /// Получение списка лотов по идентификатору аукциона
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetLotsByAuctionIdAsync([FromQuery] GetLotsByAuctionIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        // TODO общий метод с учетом Generic-оф
        if (result.IsFailed)
            return BadRequest(string.Join(", ", result.Reasons.Select(r => r.Message)));
        
        // TODO DTO
        
        return Ok(result.Value);
    }
}