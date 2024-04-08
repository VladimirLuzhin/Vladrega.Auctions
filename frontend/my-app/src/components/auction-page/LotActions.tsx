import { Button, Icon } from "@blueprintjs/core"
import { FC, useCallback } from "react"

import './LotActions.css'

type Props = {
    lotId: string
    isPurchased: boolean
    betStep: number
    maxBet: number
    buyoutPrice: number | undefined,
    onDoBetClick: (lotId: string) => Promise<void>
}

export const LotActions: FC<Props> = ({ lotId, isPurchased, betStep, maxBet, buyoutPrice, onDoBetClick }) => {
    const onDoBet = useCallback(async () => {
        await onDoBetClick(lotId)
    }, [lotId, onDoBetClick])

    return <div className='lot-actions'>
        <div className='lot-actions__bet-info'>            
            <div>Текущая максимальная ставка: {maxBet}</div>
            <div>Шаг ставки: {betStep}</div>
            {buyoutPrice && <div>Цена выкупа: {buyoutPrice}</div>}
        </div>
        {!isPurchased && <Button alignText='center' className='lot-actions__button bp4-intent-primary' onClick={onDoBet}>Ставка</Button>}
        {!isPurchased && buyoutPrice && <Button alignText='center' className='lot-actions__button bp4-intent-success' onClick={() => alert('Выкупил')}>Выкупить</Button>}
    </div>
}