using FluentResults;

namespace Vladrega.Auctions.Domain;

/// <summary>
/// Лот
/// </summary>
public class Lot
{
    /// <summary>
    /// Идентификатор лота
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Идентификатор аукциона
    /// </summary>
    public int AuctionId { get; init; }
    
    /// <summary>
    /// Название лота
    /// </summary>
    public string Name { get; init; }
    
    /// <summary>
    /// Код лота
    /// </summary>
    public string Code { get; init; }
    
    /// <summary>
    /// Описание лота
    /// </summary>
    public string Description { get; init; }
    
    /// <summary>
    /// Статус лота
    /// </summary>
    public LotStatus Status { get; init; }

    private List<Bet> _bets = new List<Bet>();

    /// <summary>
    /// Ставки по лоту
    /// </summary>
    public IReadOnlyCollection<Bet> Bets => _bets;

    /// <summary>
    /// Картинки лота
    /// </summary>
    public IReadOnlyCollection<string> Images { get; init; } = new List<string>();


    /// <summary>
    /// Попытка сделать ставку
    /// </summary>
    /// <param name="bet">Ставка</param>
    /// <returns>Результат выполнения операции. Если по лоту торги завершены или ставка с таким размером уже сделана, то вернет Fail</returns>
    public Result TryDoBet(Bet bet)
    {
        if (Status == LotStatus.Complete)
            return Result.Fail("На данный лот невозможно сделать ставку, т.к. торги завершены");

        if (_bets.Any(b => b.Amount >= bet.Amount))
            return Result.Fail("Ваша ставка была перекрыта, пожалуйста, повторите попытку");

        _bets.Add(bet);
        return Result.Ok();
    }
}