using System.Threading.Tasks;

namespace MMORPG.Repositories{
    public interface IDrop{
        public Task DropPlayerCollection();
        public Task DropQuestCollection();
    }
}