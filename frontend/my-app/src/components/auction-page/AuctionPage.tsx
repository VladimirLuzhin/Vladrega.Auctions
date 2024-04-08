import { FC, useCallback, useContext, useEffect, useMemo, useRef, useState } from 'react'
import { useParams } from 'react-router-dom'
import { AuctionContext } from '../../contexts/AuctionsContext'
import { Lot } from '../../models/Lot'
import { LotsRepository } from '../../repositories/LotsRepository'
import { LotPanel } from './LotPanel'

import './AuctionPage.css'
import { Bet } from '../../models/Bet'

export const AuctionPage: FC = () => {
    const [isLoading, setIsLoading] = useState(false)

    const { auctions, subscribeOnBetsUpdate, unsubscribeOnBetsUpdate } = useContext(AuctionContext)

    const params = useParams()
    const [lots, setLots] = useState<Lot[]>([])

    const auction = useMemo(() => {
        return auctions.get(params.id ?? '')
    }, [params, auctions])

    const updateLotBets = useCallback((lotId: string, bets: Bet[]) => {
        setLots(currentLots => {
            const copy = [...currentLots]
            const lot = copy.find(l => l.id == lotId);
            if (!lot)
                return currentLots

            lot.bets = bets
            return copy
        })
    }, [setLots])

    useEffect(() => {
        setIsLoading(true)
        LotsRepository.getLotsAsync(auction!.id)
            .then((l) => {
                setLots(l)
            })
            .catch(alert)
            .finally(() => {
                setIsLoading(false)
            })
    }, [auction])

    useEffect(() => {
        subscribeOnBetsUpdate(updateLotBets)

        return () => {
            unsubscribeOnBetsUpdate(updateLotBets)
        }
    }, [updateLotBets])

    return <>
        {
            auction && <div className='auction-page'>
                <div>{auction.name}</div>
                <div className='auction-page__body'>                    
                    {

                        lots.length > 0 && <div className='lots-list'>
                            {
                                lots.map(l => <LotPanel key={l.id} auctionId={auction.id} lot={l}/>)
                            }
                        </div>
                    }
                </div>
                <div>Footer</div>
            </div>
        }
    </>
}