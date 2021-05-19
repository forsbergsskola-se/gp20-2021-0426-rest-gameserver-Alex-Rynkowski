using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Model;
using Client.RestApi;
using Client.Utilities;

namespace Client.UserStrategy{
    public class QuestStrategy{
        public async Task PlayerQuests(Player player){
            Custom.WriteLine($"{player.Name}, what would you like to do?", ConsoleColor.White);
            while (true){
                Custom.WriteMultiLines(ConsoleColor.Yellow, "0: Go back", "1: Complete a quest",
                    "2: List all available quests");
                var userInput = Custom.ReadLine(ConsoleColor.Green);
                switch (userInput){
                    case "0":
                        return;
                    case "1":
                        var completeQuest = await CompleteQuest(player.Id);
                        if (!string.IsNullOrEmpty(completeQuest.QuestName))
                            Custom.WriteLine($"Successfully complete {completeQuest.QuestName}", ConsoleColor.White);
                        break;
                    case "2":
                        var questList = await GetAvailableQuests(player.Id);
                        foreach (var quest in questList.Where(quest => quest != null)){
                            Custom.WriteMultiLines(ConsoleColor.White, "Quest data:", $"Quest Id: {quest.QuestId}",
                                $"Quest name: {quest.QuestName}", $"Gold reward: {quest.GoldReward}",
                                $"Experience reward: {quest.ExpReward}",
                                $"Level requirement: {quest.LevelRequirement}");
                            Console.WriteLine("----------------------------");
                        }

                        break;
                    default:
                        Custom.WriteLine("Unknown input", ConsoleColor.Red);
                        break;
                }
            }
        }

        async Task<Quest> GetQuest(Guid questId)
            => await QuestRequest.GetQuest(questId);

        async Task<List<Quest>> GetAvailableQuests(Guid playerId){
            await PlayerRequest.Get(playerId);
            return await QuestRequest.GetAllQuests(playerId);
        }

        async Task<Quest> CompleteQuest(Guid playerId){
            Custom.WriteLine("Enter the quest id to complete quest", ConsoleColor.Yellow);
            try{
                var userInput = Custom.ReadLine(ConsoleColor.Green);
                var quest = await GetQuest(Guid.Parse(userInput ?? string.Empty));
                return await QuestRequest.CompleteQuest(playerId, quest);
            }
            catch (Exception){
                Custom.WriteLine("Invalid quest id", ConsoleColor.Red);
                return null;
            }
        }
    }
}