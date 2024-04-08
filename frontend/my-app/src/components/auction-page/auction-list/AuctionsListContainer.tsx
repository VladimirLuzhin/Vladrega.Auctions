import { useContext, useMemo } from "react";
import { AuctionContext } from "../../../contexts/AuctionsContext";
import { AuctionStatus } from "../../../models/AuctionStatus";
import { AuctionBlock } from "../../auction-block/AuctionBlock";
import { AuctionListPresenter } from "./AuctionListPresent";

export const AuctionsListContainer: React.FC = () => {
    const { auctions } = useContext(AuctionContext)

    const upcomingAuctions = useMemo(() => {
        return [...auctions.values()]
            .filter(a => a.status === AuctionStatus.WaitBidding)
    }, [auctions])

    const activeAuctions = useMemo(() => {
        return [...auctions.values()]
            .filter(a => a.status === AuctionStatus.Bidding)
    }, [auctions])

    const completedAuctions = useMemo(() => {
        return [...auctions.values()]
            .filter(a => a.status === AuctionStatus.Complete)
    }, [auctions])

    return <AuctionListPresenter activeAuctions={activeAuctions} completedAuctions={completedAuctions} upcomingAuctions={upcomingAuctions}/>
};
