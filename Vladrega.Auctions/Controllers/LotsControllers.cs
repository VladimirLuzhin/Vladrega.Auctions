using Microsoft.AspNetCore.Mvc;

namespace Vladrega.Auctions.Controllers;

/// <summary>
/// Контроллер для работы с лотами
/// </summary>
[ApiController]
[Route("api/v1/auctions/lots")]
public class LotsControllers : ControllerBase
{
    /// <summary>
    /// Создать лот
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateLotAsync()
    {
        return Ok();
    }

    /// <summary>
    /// Удалить лот
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteLotAsync()
    {
        return Ok();
    }

    /// <summary>
    /// Обновить лот
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateLotAsync()
    {
        return Ok();
    }

    /// <summary>
    /// Получение списка лотов по идентификатору аукциона
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetLotsByAuctionIdAsync([FromQuery] int auctionId)
    {
        return Ok();
    }
}