using System.Threading.Tasks;
using MMORPG.Items;

namespace MMORPG.Api{
    public interface IItemController{
        public Task<IItem> CreateItem(string itemType, string itemName);
    }
}