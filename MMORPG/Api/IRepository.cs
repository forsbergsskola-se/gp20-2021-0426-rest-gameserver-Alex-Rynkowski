using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MMORPG.Database;

namespace MMORPG.Api{
    public interface IRepository{
        IPlayerRepository PlayerRepository{ get; }
        IEquipRepository EquipRepository{ get; }
        IItemRepository ItemRepository{ get; }
        IQuestRepository QuestRepository{ get; }
    }
}