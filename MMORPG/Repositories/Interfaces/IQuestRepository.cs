using System;
using System.Threading.Tasks;
using MMORPG.Data;

namespace MMORPG.Repositories{
    public interface IQuestRepository{
        Task<Quest> CreateQuest(string questName, int levelRequirement);
        Task<Quest> GetQuest(Guid questId);
        Task<Quest[]> GetAllQuests();
        void AssignQuestInterval();
        Task<Player> CompleteQuest(Guid id, string questName);
    }
}