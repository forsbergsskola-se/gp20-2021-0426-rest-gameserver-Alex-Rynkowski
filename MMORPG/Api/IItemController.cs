using System;
using System.Threading.Tasks;
using MMORPG.Items;

namespace MMORPG.Api{
    public interface IItemController{
        public Task<IItem> CreateItem(Guid id, string itemName, ItemTypes itemType);
        public Task<IItem> DeleteItem(Guid id, string itemName);
    }
}