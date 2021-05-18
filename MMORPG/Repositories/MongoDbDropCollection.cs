using System.Threading.Tasks;
using MMORPG.Utilities;

namespace MMORPG.Repositories{
    /// <summary>
    /// Sole purpose of this class/interface is for Unit testing 
    /// </summary>
    public class MongoDbDropCollection : IDrop{
        public async Task DropPlayerCollection(){
            await ApiUtility.GetDatabase().DropCollectionAsync("Players");
        }

        public async Task DropQuestCollection(){
            await ApiUtility.GetDatabase().DropCollectionAsync("Quests");
        }
    }
}