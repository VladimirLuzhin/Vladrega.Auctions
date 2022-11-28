using FluentResults;
using MediatR;
using Vladrega.Auctions.Domain;

namespace Vladrega.Auctions.Application.Auctions.Create;

/// <summary>
/// Обработчик команды создания аукциона
/// </summary>
public class CreateAuctionCommandHandler : IRequestHandler<CreateAuctionCommand, Result>
{
    private readonly UnitOfWork _unitOfWork;

    public CreateAuctionCommandHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
    {
        // TODO пользователь
        var auction = new Auction(request.Name, Guid.NewGuid(), request.DateStart, request.DateEnd);

        await _unitOfWork.Auctions.SaveAsync(new[] { auction }, cancellationToken);
        return Result.Ok();
    }
}