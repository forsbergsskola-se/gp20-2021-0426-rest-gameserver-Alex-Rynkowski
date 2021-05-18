using System;
using System.Threading.Tasks;
using Client.Model;
using Newtonsoft.Json;

namespace Client.RestApi{
    public static class QuestRequest{
        public static async Task<Quest> CompleteQuest(Guid playerId, Quest quest)
            => await RestApi.Api.PostRequest<Quest>($"players/{playerId}/quests/complete", JsonConvert.SerializeObject(quest));
    }
}