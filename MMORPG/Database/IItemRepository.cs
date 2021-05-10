using System;
using System.Threading.Tasks;
using MMORPG.Items;

namespace MMORPG.Database{
    public interface IItemRepository{
        public Task<Item> CreateItem(Guid id, string itemName, ItemTypes itemType);
        public Task<Item> DeleteItem(Guid id, string itemName);
    }
}