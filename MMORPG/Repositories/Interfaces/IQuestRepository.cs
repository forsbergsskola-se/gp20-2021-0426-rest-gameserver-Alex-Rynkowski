using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MMORPG.Data;

namespace MMORPG.Repositories{
    public interface IQuestRepository{
        Task<Quest> CreateQuest(string questName, int levelRequirement);
        Task<Quest> GetQuest(Guid questId);
        Task<Quest[]> GetAllQuests();
        Task<Player> AssignQuests(Player player, DateTime lastLoginTime);
        Task<Player> CompleteQuest(Guid id, string questName);
    }
}