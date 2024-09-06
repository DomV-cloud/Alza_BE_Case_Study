using Domain.Enums;

namespace Contracts.Responses.CreateOrder
{
    public class CreatedOrderResponse
    {
        public string CustomerName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string OrderState { get; set; } = string.Empty;
        public List<CreatedOrderItemResponse> Items { get; set; } = [];
    }
}
