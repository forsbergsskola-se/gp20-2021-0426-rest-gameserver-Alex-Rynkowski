using System.Threading.Tasks;

namespace MMORPG.Repositories{
    /// <summary>
    /// Sole purpose of this class/interface is for Unit testing 
    /// </summary>
    public interface IDrop{
        public Task DropPlayerCollection();
        public Task DropQuestCollection();
    }
}