using FluentResults;
using Vladrega.Auctions.Application.Mediator;

namespace Vladrega.Auctions.Application.Auctions.Update;

/// <inheritdoc />
public class UpdateAuctionCommandValidator : IValidator<UpdateAuctionCommand>
{
    public Result Validate(UpdateAuctionCommand? request)
    {
        if (request is null)
            return Result.Fail("Не удалось распознать данные");
        
        if (string.IsNullOrWhiteSpace(request.Name))
            return Result.Fail("Передано пустое имя");
        
        if (request.DateEnd == default)
            return Result.Fail("Передана некорректная новая дата завершения аукциона");
        
        if (request.DateStart == default)
            return Result.Fail("Передана некорректная новая дата начала аукциона");
        
        if (request.DateEnd <= request.DateStart)
            return Result.Fail("Дата завершения не может быть меньше или равна даты начала аукциона");
        
        return Result.Ok();
    }
}