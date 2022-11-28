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
    public Guid Id { get; init; }
    
    /// <summary>
    /// Название лота
    /// </summary>
    public string Name { get; private set; }
    
    /// <summary>
    /// Код лота
    /// </summary>
    public string Code { get; private set; }
    
    /// <summary>
    /// Описание лота
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Шаг ставки
    /// </summary>
    public decimal BetStep { get; private set; }
    
    /// <summary>
    /// Стоимость выкупа
    /// </summary>
    public decimal? BuyoutPrice { get; private set; }
    
    private readonly List<Bet> _bets = new List<Bet>();

    /// <summary>
    /// Ставки по лоту
    /// </summary>
    public IReadOnlyCollection<Bet> Bets => _bets;

    /// <summary>
    /// Картинки лота
    /// </summary>
    public IReadOnlyCollection<string> Images { get; init; }

    /// <summary>
    /// Выкуплен ли лот
    /// </summary>
    public bool IsPurchased => _bets.Count > 0 && _bets.Max(b => b.Amount) == BuyoutPrice;
    
    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="name">Название лота</param>
    /// <param name="code">Код лота</param>
    /// <param name="description">Описание лота</param>
    /// <param name="betStep"></param>
    /// <param name="buyoutPrice"></param>
    /// <param name="images">Картинки лота</param>
    public Lot(string name, string code, string description, decimal betStep, decimal? buyoutPrice = null, params string[] images)
    {
        Id = Guid.NewGuid();
        Name = name;
        Code = code;
        Description = description;
        BetStep = betStep;
        BuyoutPrice = buyoutPrice;
        Images = images;
    }

    /// <summary>
    /// Попытка сделать ставку
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, который делает ставку</param>
    /// <returns>Результат выполнения операции. Если по лоту торги завершены или ставка с таким размером уже сделана, то вернет Fail</returns>
    public Result<Bet> TryDoBet(Guid userId)
    {
        if (IsPurchased)
            return Result.Fail("По данному лоту запрещено делать ставки, т.к. он выкуплен");

        var nextStep = _bets.Count > 0
            ? _bets.Max(b => b.Amount) + BetStep
            : BetStep;
        
        var bet = new Bet
        {
            Id = Guid.NewGuid(),
            LotId = Id,
            Amount = nextStep,
            AuthorId = userId,
            DateTime = DateTime.UtcNow
        };
        
        _bets.Add(bet);
        
        return Result.Ok(bet);
    }

    /// <summary>
    /// Обновление информации по лоту
    /// </summary>
    /// <param name="name">Название лота</param>
    /// <param name="code">Код лота</param>
    /// <param name="description">Описание лота</param>
    /// <param name="betStep">Шаг ставки</param>
    /// <param name="buyoutPrice">Стоимость выкупа</param>
    /// <returns>Результат обновления информации. Если лот уже выкуплен, то вернется Fail</returns>
    public Result UpdateInformation(string name, string code, string description, decimal betStep, decimal? buyoutPrice)
    {
        if (IsPurchased)
            return Result.Fail("По данному лоту запрещено делать ставки, т.к. он выкуплен");
        
        Name = name;
        Code = code;
        Description = description;
        BetStep = betStep;
        BuyoutPrice = buyoutPrice;
        
        return Result.Ok();
    }
}