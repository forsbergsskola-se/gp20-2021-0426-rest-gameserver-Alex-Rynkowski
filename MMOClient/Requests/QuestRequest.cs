using System;
using System.Threading.Tasks;
using Client.Model;
using Client.Utilities;
using Newtonsoft.Json;

namespace Client.Requests{
    public class QuestRequest{
        public static async Task<Quest> CompleteQuest(Guid playerId, Quest quest)
            => await ApiConnection.PostRequest<Quest>($"players/{playerId}/quests/complete", JsonConvert.SerializeObject(quest));
    }
}