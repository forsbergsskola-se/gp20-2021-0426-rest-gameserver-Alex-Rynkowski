using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MMORPG.BLL;
using MMORPG.Data;
using MMORPG.Database;

namespace MMORPG.Api{
    public interface IItemRepository{
        Task<Item> CreateItem(Guid id, ModifiedItem newItem);
        Task<Item> DeleteItem(Guid id, string itemName);
        Task<List<Item>> GetInventory(Guid id);
        Task<Item> GetItem(Guid id, string name);
        Task<Item> SellItem(Guid playerId, string itemName, IRepository repository);
    }
}