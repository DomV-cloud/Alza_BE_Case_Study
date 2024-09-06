using Application.Interfaces.Repository.ItemRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly AlzaDbContext _context;

        public ItemRepository(AlzaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Item>?> GetItemBasedOnOrderId(List<int> itemIds)
        {
            var itemList = await _context.Items.Where(item => itemIds.Contains(item.Id)).ToListAsync();

            if (itemList.Count == 0)
            {
                return [];
                throw new Exception("No items where found to this order");
            }

            return itemList;
        }
    }
}
