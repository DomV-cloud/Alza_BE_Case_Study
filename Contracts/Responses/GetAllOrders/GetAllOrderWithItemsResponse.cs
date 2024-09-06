using Domain.Enums;

namespace Contracts.Responses.GetAllOrders
{
    public class GetAllOrderWithItemsResponse
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string OrderState { get; set; } = string.Empty;
        public List<GetAllOrderItemResponse> Items { get; set; } = [];
    }
}
