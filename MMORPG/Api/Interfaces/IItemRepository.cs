using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MMORPG.Database;

namespace MMORPG.Api{
    public interface IItemRepository{
        Task<Player> CreateItem(Guid id, ModifyItem modifyItem);
        Task<Item> DeleteItem(Guid id, string itemName);
        Task<List<Item>> GetInventory(Guid id);
        Task<Item> GetItem(Guid id, string name);
        Task<Item> SellItem(Guid id, string itemName);
    }
}