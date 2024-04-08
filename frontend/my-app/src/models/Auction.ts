import { AuctionStatus } from "./AuctionStatus"

export type Auction = {
    id: string
    name: string
    dateStart: string
    dateEnd: string
    status: AuctionStatus
    lotPictures: string[]
}