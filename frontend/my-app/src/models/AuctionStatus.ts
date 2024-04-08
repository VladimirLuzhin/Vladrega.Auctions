export enum AuctionStatus {
    /** Этап создания ауцкциона */
    Creation = 1,
    
    /** Этап ожидания торгов */
    WaitBidding = 2,
    
    /** Идут торги */
    Bidding = 3,
    
    /** Аукцион завершен */
    Complete = 4,
    
    /** Аукцион отменен */
    Canceled = 5
}