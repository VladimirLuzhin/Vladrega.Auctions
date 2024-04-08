import { FC, useCallback, useContext, useEffect, useMemo, useRef, useState } from 'react'
import { Lot } from '../../models/Lot'
import { LotActions } from './LotActions'
import { LotAvatar } from './LotAvatar'
import { Card, Elevation } from "@blueprintjs/core";

import './LotPanel.css'
import { AuctionContext } from '../../contexts/AuctionsContext';

type Props = {
    auctionId: string
    lot: Lot
}

export const LotPanel: FC<Props> = ({ auctionId, lot }) => {
    const { onDoBet } = useContext(AuctionContext)

    const onDoBetClick = useCallback(async (lotId: string) => {
        await onDoBet(auctionId, lotId)
    }, [auctionId, lot, onDoBet])

    const maxBet = Math.max(0, ...lot.bets.map(b => b.amount))
    const image = lot.images.length > 0
        ? lot.images[0]
        : ''

    return <Card 
        interactive={true} 
        elevation={Elevation.TWO}
        className={'lot-panel'}
    >
        <div className='lot-panel__body'>
            <LotAvatar 
                lotImage={image} 
                lotCode={lot.code} 
            />        
            <div className='lot-panel__name'>Название лота: {lot.name}</div>
            <LotActions 
                lotId={lot.id} 
                betStep={lot.betStep}
                maxBet={maxBet}
                buyoutPrice={lot.buyoutPrice}
                isPurchased={lot.isPurchased}
                onDoBetClick={onDoBetClick}
            />
        </div>
    </Card>
}