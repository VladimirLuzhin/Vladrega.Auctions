using FluentResults;

namespace Vladrega.Auctions.Domain;

/// <summary>
/// Аукцион
/// </summary>
public class Auction
{
    private readonly Dictionary<Guid, Lot> _lots = new();
    
    /// <summary>
    /// Идентификатор аукциона
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();
    
    /// <summary>
    /// Название аукциона
    /// </summary>
    public string Name { get; private set; }
    
    /// <summary>
    /// Автор аукциона
    /// </summary>
    public Guid AuthorId { get; init; }
    
    /// <summary>
    /// Дата начала аукциона
    /// </summary>
    public DateTime DateStart { get; private set; }

    /// <summary>
    /// // TODO подумать над флаговым ENUM-ом
    /// Флаг, оформляется ли аукциона сейчас
    /// </summary>
    public bool IsCreation { get; private set; }
    
    /// <summary>
    /// Флаг, отменен ли ауцкион
    /// </summary>
    public bool IsCanceled { get; private set; }
    
    private DateTime _dateEnd;

    /// <summary>
    /// Дата завершения аукциона
    /// </summary>
    public DateTime DateEnd
    {
        get
        {
            if (_lots.Count == 0)
                return _dateEnd;
            
            var lotsWithBets = _lots.Values.Where(b => b.Bets.Count > 0).ToArray();
            if (lotsWithBets.Length == 0)
                return _dateEnd;
            
            // Логика автопродления аукциона. Если ставка сделана за 30 или менее секунд до конца аукциона, продлеваем его на 30 секунд,
            // воизбежание ситуаций, когда пользователи будут пытаться сделать ставку под конец аукциона.
            var maxBetDate = lotsWithBets.SelectMany(l => l.Bets).Max(s => s.DateTime).AddSeconds(30);
            return _dateEnd > maxBetDate ? _dateEnd : maxBetDate;
        }
        private set => _dateEnd = value;
    }

    /// <summary>
    /// Статус аукциона
    /// </summary>
    public AuctionStatus Status
    {
        get
        {
            if (IsCanceled)
                return AuctionStatus.Canceled;
            
            if (IsCreation)
                return AuctionStatus.Creation;

            var dateTimeNow = DateTime.UtcNow;
            if (dateTimeNow < DateStart)
                return AuctionStatus.WaitBidding;

            if (_lots.Count > 0 && _lots.Values.All(l => l.IsPurchased))
                return AuctionStatus.Complete;
            
            if (dateTimeNow > DateStart && dateTimeNow < DateEnd)
                return AuctionStatus.Bidding;

            return AuctionStatus.Complete;
        }
    }

    /// <summary>
    /// Проверка, можно ли менять состояние аукциона
    /// </summary>
    public bool IsEditable => Status is not (AuctionStatus.Bidding or AuctionStatus.Complete or AuctionStatus.Canceled);

    /// <summary>
    /// Лоты по аукциону
    /// </summary>
    public IReadOnlyDictionary<Guid, Lot> Lots => _lots;

    /// <summary>
    /// .ctor
    /// </summary>
    public Auction()
    {
    }


    /// <summary>
    /// Конструктор для начального создания аукциона
    /// </summary>
    /// <param name="name">Название аукциона</param>
    /// <param name="authorId">Идентификатор автора</param>
    /// <param name="dateStart">Дата начала аукциона</param>
    /// <param name="dateEnd">Дата завершения аукциона</param>
    public Auction(string name, Guid authorId, DateTime dateStart, DateTime dateEnd)
    {
        Name = name;
        AuthorId = authorId;
        DateStart = dateStart;
        DateEnd = dateEnd;
        IsCreation = true;
    }

    /// <summary>
    /// Обновление информации по аукциону
    /// </summary>
    /// <param name="newName">Новое название аукциона</param>
    /// <param name="newDateStart">Новая дата начала аукциона</param>
    /// <param name="newDateEnd">Новая дата завершения ауцкиона</param>
    public Result UpdateInformation(string newName, DateTime newDateStart, DateTime newDateEnd)
    {
        if (!IsEditable)
            return Result.Fail("Нельзя редактировать ауцкион");
        
        Name = newName;
        DateStart = newDateStart;
        DateEnd = newDateEnd;
        
        return Result.Ok();
    }

    /// <summary>
    /// Измнения статуса оформления ауцкиона на противоположный
    /// </summary>
    public Result ChangeCreationState()
    {
        // TODO
        IsCreation = !IsCreation;
        return Result.Ok();
    }

    /// <summary>
    /// Метод отмены аукциона. Меняет состояние флага IsCanceled в true
    /// </summary>
    public Result Cancel()
    {
        if (IsEditable)
            return Result.Fail("Отменить можно только нередактируемый ауцкион");
        
        IsCanceled = true;
        return Result.Ok();
    }
    

    /// <summary>
    /// Добавление лота в аукцион
    /// </summary>
    /// <param name="name">Название лота</param>
    /// <param name="code">Код лота</param>
    /// <param name="description">Описание лота</param>
    /// <param name="betStep">Шаг ставки лота</param>
    /// <param name="buyoutPrice">Стоимость выкупа лота</param>
    /// <returns>Результат добавления лота в ауцкион</returns>
    public Result<Lot> AddLot(string name, string code, string description, decimal betStep, decimal? buyoutPrice, params string[] images)
    {
        if (!IsEditable)
            return Result.Fail("Данный аукцион нельзя редактировать");

        var lot = new Lot(name, code, description, betStep, buyoutPrice, images);
        _lots.Add(lot.Id, lot);
        
        return Result.Ok(lot);
    }

    /// <summary>
    /// Удаление лота из аукциона
    /// </summary>
    /// <param name="lotId">Идентификатор лота</param>
    public Result<Lot> RemoveLot(Guid lotId)
    {
        if (!IsEditable)
            return Result.Fail("Данный аукцион нельзя редактировать");

        if (!_lots.Remove(lotId, out var lot))
            return Result.Fail("В указанном аукционе не найден лот с переданным идентификатором");

        return Result.Ok(lot);
    }

    /// <summary>
    /// Обновить информацию по лоту
    /// </summary>
    /// <param name="lotId">Идентификатор лота</param>
    /// <param name="name">Название лота</param>
    /// <param name="code">Код лота</param>
    /// <param name="description">Описание лота</param>
    /// <param name="betStep">Шаг ставки</param>
    /// <param name="buyoutPrice">Стоимость выкупа</param>
    public Result<Lot> UpdateLot(Guid lotId, string name, string code, string description, decimal betStep, decimal? buyoutPrice)
    {
        if (!IsEditable)
            return Result.Fail("Данный аукцион нельзя редактировать");
        
        if (!Lots.TryGetValue(lotId, out var lot))
            return Result.Fail("В указанном аукционе не найден лот с переданным идентификатором");
        
        var result = lot.UpdateInformation(name, code, description, betStep, buyoutPrice);
        if (result.IsFailed)
            return result;
        
        return lot;
    }

    /// <summary>
    /// Сделать ставку по лоту ауцкиона от лица пользователя
    /// </summary>
    /// <param name="lotId">Идентификатор лота</param>
    /// <param name="userId">Идентификатор пользователя</param>
    public Result<Bet> DoBet(Guid lotId, Guid userId)
    {
        if (IsEditable)
            return Result.Fail("По данному аукциону нельзя сделать ставку, т.к. он редактируется");
        
        if (!Lots.TryGetValue(lotId, out var lot))
            return Result.Fail("В указанном аукционе не найден лот с переданным идентификатором");

        return lot.TryDoBet(userId);
    }
}