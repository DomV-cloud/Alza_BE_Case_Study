namespace Contracts.Requests.PayOrder
{
    public class PayOrderRequest
    {
        public string OrderNumber { get; set; } = string.Empty;

        public bool IsPaid { get; set; }
    }
}
