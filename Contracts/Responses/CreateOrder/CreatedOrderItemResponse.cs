namespace Contracts.Responses.CreateOrder
{
    public class CreatedOrderItemResponse
    {
        public string ItemName { get; set; } = string.Empty;
        public int NumberOfItems { get; set; }
        public decimal Price { get; set; }
    }
}
