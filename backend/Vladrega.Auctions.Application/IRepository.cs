namespace Vladrega.Auctions.Application;

/// <summary>
/// Базовая абстракция всех репозиториев
/// </summary>
/// <typeparam name="T">Тип объекта, с которым работает репозиторий</typeparam>
public interface IRepository<T>
{
    /// <summary>
    /// Сохранение объектов
    /// </summary>
    /// <param name="objects">Объекты для сохранения</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task SaveAsync(IEnumerable<T> objects, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление объектов
    /// </summary>
    /// <param name="objects">Объекты для удаления</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task DeleteAsync(IEnumerable<T> objects, CancellationToken cancellationToken);

    /// <summary>
    /// Получение элементов
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task<IReadOnlyCollection<T>> GetCollectionAsync(CancellationToken cancellationToken);
}