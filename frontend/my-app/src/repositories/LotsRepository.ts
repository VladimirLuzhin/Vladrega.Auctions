import { Lot } from '../models/Lot'

interface ILotsRepository {
    getLotsAsync(auctionId: string): Promise<Lot[]>
}

class HttpLotsRepository implements ILotsRepository {
    async getLotsAsync(auctionId: string): Promise<Lot[]> {
        const response = await fetch(`https://localhost:7217/api/v1/auctions/lots?auctionId=${auctionId}`)
        const json = await response.json()

        return json as Lot[]
    }
}

export const LotsRepository: ILotsRepository = new HttpLotsRepository();