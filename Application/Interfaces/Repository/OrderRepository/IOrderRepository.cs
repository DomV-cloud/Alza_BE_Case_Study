using Domain.Entities;

namespace Application.Interfaces.Repository.OrderRepository
{
    public interface IOrderRepository
    {
        public Task<Order?> CreateOrder(Order order);

        public Task<List<Order>> GetAllOrders();

        public Task<Order?> UpdateOrderState(string orderNumber, bool isPaid);
    }
}
