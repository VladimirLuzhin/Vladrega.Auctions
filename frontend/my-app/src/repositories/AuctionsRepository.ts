import { Auction } from "../models/Auction";

interface IAuctionsRepository {
    getAuctionsAsync(): Promise<Auction[]>
}

class HttpAuctionsRepository implements IAuctionsRepository {
    async getAuctionsAsync(): Promise<Auction[]> {
        const response = await fetch('https://localhost:7217/api/v1/auctions')
        const json = await response.json()

        const auctions = json as Auction[]
        return auctions
    }
}

export const auctionsRepository : IAuctionsRepository = new HttpAuctionsRepository()