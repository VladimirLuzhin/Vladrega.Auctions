using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vladrega.Auctions.Application.Auctions.CreateAuction;

namespace Vladrega.Auctions.Controllers;

/// <summary>
/// Контроллер для работы с аукционом
/// </summary>
[ApiController]
[Route("api/v1/auctions")]
public class AuctionController : ControllerBase
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
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAuctionAsync(CreateActionCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        if (response.IsFailed)
            return BadRequest(string.Join(", ", response.Reasons.Select(r => r.Message))); 
                
        return Ok();
    }

    /// <summary>
    /// Отмена аукциона
    /// </summary>
    /// <param name="id">Идентификатор аукциона</param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> CancelAuctionAsync(int id)
    {
        return Ok();
    }

    /// <summary>
    /// Обновление аукциона
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateAuctionAsync()
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAuctionsAsync()
    {
        return Ok();
    }
}