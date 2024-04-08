interface IBetRepository {
    doBetAsync(auctionId: string, lotId: string): Promise<void>
}

class HttpBetRepsotiry implements IBetRepository {
    async doBetAsync(auctionId: string, lotId: string): Promise<void> {
        const response = await fetch('https://localhost:7217/api/v1/auctions/lots/bets', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                auctionId: auctionId,
                lotId: lotId
            })
        })

        if (!response.ok)
            throw new Error('Ошибка при попытке сделать ставку')
    }

}

export const BetRepository : IBetRepository = new HttpBetRepsotiry()