namespace Vladrega.Auctions.Domain;

/// <summary>
/// Пользователь
/// </summary>
public class User
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; init; }
    
    /// <summary>
    /// Имейл пользователя
    /// </summary>
    public string Email { get; init; }
}