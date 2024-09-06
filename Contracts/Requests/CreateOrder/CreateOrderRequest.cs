namespace Contracts.Requests.CreateOrder
{
    public class CreateOrderRequest
    {
        public string OrderNumber { get; set; } = string.Empty;
        public List<int> ProductIds { get; set; } = [];
        public string CustomerName { get; set; } = string.Empty;
    }

}
