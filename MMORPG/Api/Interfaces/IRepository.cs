using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMORPG.Api{
    public interface IRepository{
        IPlayerRepository PlayerRepository{ get; }
        IEquipRepository EquipRepository{ get; }
        IItemRepository ItemRepository{ get; }
        IQuestRepository QuestRepository{ get; }
    }
}