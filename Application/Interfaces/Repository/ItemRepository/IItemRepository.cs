using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository.ItemRepository
{
    public interface IItemRepository
    {
        public Task<List<Item>?> GetItemBasedOnOrderId(List<int> itemIds);
    }
}
