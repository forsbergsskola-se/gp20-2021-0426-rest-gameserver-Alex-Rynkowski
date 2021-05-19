using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Model;
using Newtonsoft.Json;

namespace Client.RestApi{
    public static class QuestRequest{
        public static async Task<Quest> CompleteQuest(Guid playerId, Quest quest)
            => await Api.PostRequest<Quest>($"players/{playerId}/quests/complete",
                JsonConvert.SerializeObject(quest));

        public static async Task<List<Quest>> GetAllQuests(Guid playerId)
            => await Api.GetResponse<List<Quest>>($"players/{playerId}/quests/getAllQuests");

        public static async Task<Quest> GetQuest(Guid questId)
            => await Api.GetResponse<Quest>($"quests/getQuest/{questId}");
    }
}