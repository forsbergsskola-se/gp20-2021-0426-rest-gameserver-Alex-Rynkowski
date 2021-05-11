using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MMORPG.Items;

namespace MMORPG.Database{
    public interface IItemRepository{
        public Task<Item> CreateItem(Guid id, string itemName, ItemTypes itemType);
        public Task<Item> DeleteItem(Guid id, string itemName);
        public Task<List<Item>> GetInventory(Guid id);
        public Task<Item> GetItem(Guid id,string name);

        public Task<Item> SellItem(Guid id, string itemName);
    }
}