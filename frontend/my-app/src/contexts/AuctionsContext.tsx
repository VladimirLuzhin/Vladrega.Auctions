import { createContext, FC, PropsWithChildren, ReactNode, useCallback, useEffect, useState } from "react"
import { Auction } from "../models/Auction"
import { AuctionStatus } from "../models/AuctionStatus"
import { auctionsRepository } from "../repositories/AuctionsRepository"
import { BetRepository } from "../repositories/BetsRepository"
import { notificator } from "../signalr/Notificator"
import { Bet } from "../models/Bet"

interface IAuctionContext {
    auctions: Map<string, Auction>,
    onDoBet: (auctionId: string, lotId: string) => Promise<void>,
    subscribeOnBetsUpdate: (callback: (lotId: string, bets: Bet[]) => void) => void
    unsubscribeOnBetsUpdate: (callback: (lotId: string, bets: Bet[]) => void) => void
}

export const AuctionContext = createContext<IAuctionContext>({
    auctions: new Map<string, Auction>(),
    onDoBet: () => {
        throw new Error('Контекст не проинициализирован')
    }, 
    subscribeOnBetsUpdate: () => {
        throw new Error('Контекст не проинициализирован')
    },
    unsubscribeOnBetsUpdate: () => {
        throw new Error('Контекст не проинициализирован')
    },  
})

export const AuctionContextProvider: FC<PropsWithChildren> = ({ children }) => {
    const [auctions, setAuctions] = useState<Map<string, Auction>>(new Map())

    const subscribeOnBetsUpdate = useCallback((callback: (lotId: string, bets: Bet[]) => void) => {
        notificator.subscribeOnBets(callback)
    }, [auctions])

    const unsubscribeOnBetsUpdate = useCallback((callback: (lotId: string, bets: Bet[]) => void) => {
        notificator.unsubscribeOnBets(callback)
    }, [auctions])

    useEffect(() => {
        notificator.startAsync()
            .then(() => {
                console.log('connected to signalr hub')
            })
            .catch((e) => {
                console.error(`error while connecting to signalr hub: ${e}`)
            })

        // return () => {
        //     notificator.stopAsync()
        // }
    }, [])

    // TODO react-query
    useEffect(() => {
        auctionsRepository.getAuctionsAsync()
            .then(auctions => setAuctions(new Map<string, Auction>(auctions.map(a => [a.id, a]))))
            .catch(e => console.error(e))
    }, [])

    return <AuctionContext.Provider value={{
        auctions: auctions,
        // TODO
        onDoBet: BetRepository.doBetAsync,
        subscribeOnBetsUpdate: subscribeOnBetsUpdate,
        unsubscribeOnBetsUpdate: unsubscribeOnBetsUpdate
    }}>
        {children}
    </AuctionContext.Provider>
}