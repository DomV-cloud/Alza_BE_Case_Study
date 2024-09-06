using Application.Interfaces.Repository.OrderRepository;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AlzaDbContext _context;

        public OrderRepository(AlzaDbContext alzaDbContext)
        {
            _context = alzaDbContext;
        }

        public async Task<Order?> CreateOrder(Order order)
        {
            if (order is null)
            {
                return null;
                throw new ArgumentNullException(nameof(order));
            }

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public Task<List<Order>> GetAllOrders()
        {
            return _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Item)
                    .ToListAsync();
        }

        public async Task<Order?> UpdateOrderState(string orderNumber, bool isPaid)
        {
            var orderToUpdate = await _context.Orders.FirstOrDefaultAsync(x => x.OrderNumber.Equals(orderNumber));

            if (orderToUpdate is null)
            {
                return null;
            }

            orderToUpdate.OrderState = isPaid ? OrderState.Paid : OrderState.Canceled;

            await _context.SaveChangesAsync();

            return orderToUpdate;
        }
    }
}
