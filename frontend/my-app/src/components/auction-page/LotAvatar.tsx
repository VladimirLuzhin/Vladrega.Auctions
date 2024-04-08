import { FC } from "react"

import './LotAvatar.css'

type Props = {
    lotImage: string
    lotCode: string
}

export const LotAvatar: FC<Props> = ({ lotImage, lotCode }) => {
    return <div className='lot-avatar'>
        <img className='lot-avatar__image' src={lotImage} />
        <div className='lot-avatar__code'>{lotCode}</div>
    </div>
}