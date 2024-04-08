import { FC, useCallback, useEffect, useMemo, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import Slider from 'react-slick'
import { Auction } from '../../models/Auction'
import { AuctionStatus } from '../../models/AuctionStatus'

import 'slick-carousel/slick/slick.css'
import 'slick-carousel/slick/slick-theme.css'
import './AuctionBlock.css'

type Props = {
    auction: Auction
}

export const AuctionBlock: FC<Props> = ({ auction }) => {
    const navigate = useNavigate();
    const [secondsLeft, setLeft] = useState((Date.parse(auction.dateEnd) - Date.parse(auction.dateStart)) / 1000)

    const timeLeft = useMemo(() => {
        let time = secondsLeft
        const hours = Math.floor(time / 3600)
        time = time - hours * 3600

        const minutes = Math.floor(time / 60)
        const seconds = time - minutes * 60

        return `${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`
    }, [secondsLeft])

    useEffect(() => {
        const interval = setInterval(() => {
            setLeft(prev => {
                if (prev === 0)
                    return prev
                
                return prev - 1
            })
        }, 1000)

        return () => clearInterval(interval)
    }, [auction])

    const onClick = useCallback(() => {
        navigate(`/auction/${auction.id}`)
    }, [auction])

    return <div className='auction-block' onClick={onClick}>        
        <div className='auctions-lots-pictures'>
            {auction.lotPictures.map(p => <img key={p} className='auction-preview' src={p} />)[0]}
            {/* <Slider 
                arrows={false}
                infinite={true} 
                dots={false} 
                slidesToShow={1} 
                slidesToScroll={1} 
                autoplay={true} 
                autoplaySpeed={5000} 
                cssEase='linear'
            >
                {)}
            </Slider> */}
        </div>
        <div className='auction-block-name' title={auction.name}>
            {auction.name}
        </div>
        <div className='auction-tags'>
            {/*  */}
            <AuctionTag text='PS5'/>
            <AuctionTag text='Computers'/>
            <AuctionTag text='Streaming'/>
        </div>
        {secondsLeft > 0 && auction.status === AuctionStatus.Bidding && <div className='auction-timer'>{timeLeft}</div>}
    </div>
}

type AuctionTagProps = {
    text: string
}

const AuctionTag : React.FC<AuctionTagProps> = ({ text }) => {
    return <div className='auction-tag'>
        {text}
    </div>
}