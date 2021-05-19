using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Model;
using Client.RestApi;
using Client.Utilities;

namespace Client.UserStrategy{
    public class PlayerStrategy{
        public async void ChooseCharacter(){
            Custom.WriteLine("\"Q[q]uit\" to stop the application from running", ConsoleColor.Yellow);
            var player = new Player();
            var playerList = new List<Player>();
            while (true){
                Custom.WriteLine("What would you like to do:", ConsoleColor.Yellow);
                Custom.WriteMultiLines(ConsoleColor.Yellow, "1: Create Character",
                    "2: Get Character", "3: Get all Characters", "4: Peek at leaderboard", "5: Get statistics");
                var userInput = Custom.ReadLine(ConsoleColor.Green);
                Custom.Exit(userInput);

                switch (userInput){
                    case "1":
                        player = await CreateCharacter();
                        break;
                    case "2":
                        player = await GetPlayer();
                        await PickStrategy(player);
                        break;
                    case "3":
                        playerList = await GetAllPlayers();
                        break;
                    case "4":
                        var leaderboardStrategy = new LeaderboardStrategy();
                        await leaderboardStrategy.Leaderboard();
                        break;
                    case "5":
                        var statistics = new StatisticsStrategy();
                        await statistics.Statistics();
                        break;
                }
            }
        }

        async Task PickStrategy(Player player){
            Custom.WriteLine($"Hello {player.Name}, what would you like to do?", ConsoleColor.White);
            while (true){
                Custom.WriteMultiLines(ConsoleColor.Yellow,
                    "0: Go back", "1: Check out items",
                    "2: Check out equipment", "3: Check out quests", "4: Modify Player");
                var userInput = Custom.ReadLine(ConsoleColor.Green);
                switch (userInput){
                    case "0":
                        return;
                    case "1":
                        var itemStrategy = new ItemStrategy();
                        await itemStrategy.PlayerItems(player);
                        break;
                    case "2":
                        var equipStrategy = new EquipStrategy();
                        await equipStrategy.PlayerEquippedItems(player);
                        break;
                    case "3":
                        var questStrategy = new QuestStrategy();
                        await questStrategy.PlayerQuests(player);
                        break;
                    case "4":
                        player = await ModifyPlayer(player.Id);
                        Custom.WriteMultiLines(ConsoleColor.White, $"New values for player: {player.Name}",
                            $"-Gold: {player.Gold}",
                            $"-Level: {player.Level}", $"-Score: {player.Score}");
                        break;
                    default:
                        Custom.WriteLine("Unknown input", ConsoleColor.Red);
                        break;
                }
            }
        }

        static async Task<Player> GetPlayer(){
            Custom.WriteLine("Give me a username or user ID:", ConsoleColor.Yellow);
            var userInput = Custom.ReadLine(ConsoleColor.Green);
            if (Guid.TryParse(userInput, out var result)){
                return await PlayerRequest.Get(result);
            }

            return await PlayerRequest.Get(userInput);
        }

        static async Task<List<Player>> GetAllPlayers(){
            var players = await PlayerRequest.GetAll();
            foreach (var getPlayer in players){
                Custom.WriteMultiLines(ConsoleColor.White,
                    $"Id: {getPlayer.Id}", $"Name: {getPlayer.Name}", $"Level: {getPlayer.Level}");
                Console.WriteLine("----------------------------------");
            }

            return players;
        }

        static async Task<Player> ModifyPlayer(Guid playerId){
            try{
                var modifiedPlayer = new ModifiedPlayer();
                Custom.WriteLine("Gold:", ConsoleColor.Yellow);
                modifiedPlayer.Gold = int.Parse(Custom.ReadLine(ConsoleColor.Green) ?? string.Empty);
                Custom.WriteLine("Level:", ConsoleColor.Yellow);
                modifiedPlayer.Level = int.Parse(Custom.ReadLine(ConsoleColor.Green) ?? string.Empty);
                Custom.WriteLine("Score:", ConsoleColor.Yellow);
                modifiedPlayer.Score = int.Parse(Custom.ReadLine(ConsoleColor.Green) ?? string.Empty);
                return await PlayerRequest.Modify(playerId, modifiedPlayer);
            }
            catch (Exception){
                Custom.WriteLine("Invalid input", ConsoleColor.Red);
                return null;
            }
        }

        static async Task<Player> CreateCharacter(){
            Custom.WriteLine("Character name:", ConsoleColor.Yellow);
            var userInput = Custom.ReadLine(ConsoleColor.Green);
            var createdPlayer = await PlayerRequest.Create(userInput);
            Custom.WriteMultiLines(ConsoleColor.White, "Created player:", $"Id: {createdPlayer.Id}",
                $"Name: {createdPlayer.Name}", $"Level: {createdPlayer.Level}");
            return createdPlayer;
        }
    }
}