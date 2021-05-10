using System.Threading.Tasks;

namespace MMORPG.Items{
    public interface IItemRepository{
        public Task<IItem> Create<T>(string itemType, string itemName) where T : new();
    }
}