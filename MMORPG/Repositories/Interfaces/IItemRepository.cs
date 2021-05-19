using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MMORPG.Data;

namespace MMORPG.Repositories{
    public interface IItemRepository{
        Task<Item> CreateItem(Guid id, ModifiedItem newItem);
        Task<List<Item>> GetInventory(Guid id);
        Task<Item> GetItem(Guid id, string name);
        Task<Item> SellItem(Guid playerId, string itemName);
    }
}