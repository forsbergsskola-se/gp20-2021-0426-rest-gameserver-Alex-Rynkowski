using System;
using System.Threading.Tasks;
using Client.Api;
using Client.Model;
using Client.RestApi;
using Client.Utilities;
using Newtonsoft.Json;

namespace Client.Requests{
    public static class QuestRequest{
        public static async Task<Quest> CompleteQuest(Guid playerId, Quest quest)
            => await RestApi.Api.PostRequest<Quest>($"players/{playerId}/quests/complete", JsonConvert.SerializeObject(quest));
    }
}