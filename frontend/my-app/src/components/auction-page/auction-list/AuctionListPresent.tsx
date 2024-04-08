import { FC } from "react";
import { Auction } from "../../../models/Auction";
import { AuctionBlock } from "../../auction-block/AuctionBlock";

type Props = {
    activeAuctions: Auction[],
    upcomingAuctions: Auction[],
    completedAuctions: Auction[]
}

export const AuctionListPresenter : FC<Props> = ({ activeAuctions, upcomingAuctions, completedAuctions }) => {
    return <>
        <div className="active-auctions">
            <h2>
                Активные аукционы
            </h2>
            <div className="list-of-auctions">
                {activeAuctions.map(a => <AuctionBlock key={a.id} auction={a}/>)}
            </div>
        </div>
        <div className="upcoming-auctions">
            <h2>
                Ожидающие торгов аукционы
            </h2>
            <div>
                {upcomingAuctions.map(a => <AuctionBlock key={a.id} auction={a}/>)}
            </div>
        </div>
        <div className="upcoming-auctions">
            <h2>
                Завершенные аукционы
            </h2>
            <div>
                {completedAuctions.map(a => <AuctionBlock key={a.id} auction={a}/>)}
            </div>
        </div>
    </>
    
}