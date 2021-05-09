using System.Threading.Tasks;

namespace MMORPG.Items{
    public interface IItemRepository{
        public Task<IItem> Create(string itemType);
    }
}