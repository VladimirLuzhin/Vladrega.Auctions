using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Vladrega.Auctions.Controllers;

/// <summary>
/// Базовый контроллер для все API контроллеров в проекте
/// </summary>
[ApiController]
public class BaseController : ControllerBase
{
    /// <summary>
    /// Метод для преобразования результата выполнения операции в HTTP ответ
    /// </summary>
    /// <param name="result">Результат выполнения операции</param>
    protected IActionResult ConvertToActionResult(ResultBase result)
    {
        if (result.IsFailed)
            return BadRequest(string.Join(", ", result.Reasons.Select(r => r.Message)));
        
        return Ok();
    }
}