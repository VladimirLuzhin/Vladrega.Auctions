using FluentResults;
using MediatR;

namespace Vladrega.Auctions.Application.Auctions.CreateAuction;

/// <summary>
/// Обработчик команды создания аукциона
/// </summary>
public class CreateAuctionCommandHandler : IRequestHandler<CreateActionCommand, Result>
{
    /// <inheritdoc />
    public Task<Result> Handle(CreateActionCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Result.Ok());
    }
}