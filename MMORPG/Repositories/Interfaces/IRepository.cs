using System.Collections.Generic;
using System.Threading.Tasks;
using MMORPG.Data;

namespace MMORPG.Repositories{
    public interface IRepository{
        IPlayerRepository PlayerRepository{ get; }
        IEquipRepository EquipRepository{ get; }
        IItemRepository ItemRepository{ get; }
        IQuestRepository QuestRepository{ get; }
        
        IStatistics Statistics{ get; }
        ILeaderboard Leaderboard{ get; }
        
        IDrop DropCollection{ get; }
    }
}