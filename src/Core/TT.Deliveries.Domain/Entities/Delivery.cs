namespace TT.Deliveries.Data.Dto
{
    public class Delivery : BaseEntity
    {
        public DeliveryState State { get; set; }
        public AccessWindow? AccessWindow { get; set; }
        public Recipient? Recipient { get; set; }
        public Order? Order { get; set; }
    }
}