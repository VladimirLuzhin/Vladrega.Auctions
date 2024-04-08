using Microsoft.AspNetCore.Mvc;

namespace Vladrega.Auctions.Controllers;

/// <summary>
/// Контроллер работы с пользователем
/// </summary>
[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    /// <summary>
    /// Авторизовать пользователя
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AuthAsync()
    {
        return Ok();
    }
}