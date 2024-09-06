namespace Domain.Entities
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ItemId { get; set; }
        public Item Item { get; set; } = null!;
    }
}
