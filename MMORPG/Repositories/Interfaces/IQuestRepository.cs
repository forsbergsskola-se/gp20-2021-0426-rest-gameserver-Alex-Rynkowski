using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MMORPG.Data;

namespace MMORPG.Repositories{
    public interface IQuestRepository{
        Task<Quest> CreateQuest(string questName, int levelRequirement);
        Task<Quest> GetQuest(Guid questId);
        Task<Quest[]> GetAllQuests(Guid playerId);
        Task<Player> AssignQuests(Player player, DateTime lastLoginTime);
        Task<Quest> CompleteQuest(Guid id, Quest quest);
    }
}