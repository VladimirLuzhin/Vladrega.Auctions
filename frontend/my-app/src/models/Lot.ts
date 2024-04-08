import { Bet } from "./Bet"

export type Lot = {
    id: string
    name: string
    code: string
    description: string
    betStep: number
    buyoutPrice?: number | undefined
    isPurchased: boolean
    bets: Bet[]
    images: string[]
}