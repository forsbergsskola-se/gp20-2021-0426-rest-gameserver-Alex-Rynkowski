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
                    "2: Get Character", "3: Get all Characters");
                var userInput = Custom.ReadLine(ConsoleColor.Green);
                Custom.Exit(userInput);

                switch (userInput){
                    case "1":
                        player = await CreateCharacter(player);
                        break;
                    case "2":
                        player = await GetPlayer(player);
                        await PickStrategy(player);
                        break;
                    case "3":
                        playerList = await GetAllPlayers(player);
                        break;
                }
            }
        }
        async Task PickStrategy(Player player){
            Custom.WriteLine($"Hello {player.Name}, what would you like to do?", ConsoleColor.White);
            while (true){
                Custom.WriteMultiLines(ConsoleColor.Yellow,
                    "0: Go back", "1: Check items",
                    "2: Check out equipment", "3: Check out quests");
                var userInput = Custom.ReadLine(ConsoleColor.Green);
                switch (userInput){
                    case "0":
                        return;
                    case "1":
                        var itemStrategy = new ItemStrategy();
                        await itemStrategy.PlayerItems(player);
                        Console.WriteLine("Doing something item related");
                        break;
                    case "2":
                        Console.WriteLine("Doing something equipment related");
                        break;
                    case "3":
                        Console.WriteLine("Doing something quest related");
                        break;
                    default:
                        Custom.WriteLine("Unknown input", ConsoleColor.Red);
                        break;
                }
            }
        }
        
        static async Task<Player> GetPlayer(Player player){
            Custom.WriteLine("Give me a username or user ID:", ConsoleColor.Yellow);
            var userInput = Custom.ReadLine(ConsoleColor.Green);
            if (Guid.TryParse(userInput, out var result)){
                return await PlayerRequest.Get(result);
            }

            return await PlayerRequest.Get(userInput);
        }

        static async Task<List<Player>> GetAllPlayers(Player player){
            var players = await PlayerRequest.GetAll();
            foreach (var getPlayer in players){
                Custom.WriteMultiLines(ConsoleColor.White,
                    $"Id: {getPlayer.Id}", $"Name: {getPlayer.Name}", $"Level: {getPlayer.Level}");
            }

            return players;
        }

        static async Task<Player> CreateCharacter(Player player){
            Custom.WriteLine("Character name:", ConsoleColor.Yellow);
            var userInput = Custom.ReadLine(ConsoleColor.Green);
            var createdPlayer = await PlayerRequest.Create(userInput);
            Custom.WriteMultiLines(ConsoleColor.White, "Created player:", $"Id: {createdPlayer.Id}",
                $"Name: {createdPlayer.Name}", $"Level: {createdPlayer.Level}");
            return createdPlayer;
        }
    }
}