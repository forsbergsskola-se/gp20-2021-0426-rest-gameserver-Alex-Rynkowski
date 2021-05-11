using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Api;
using Client.Utilities;

//Color codes:
// Output White
// Yellow instructions
// Green user input
// red error or exception
namespace Client{
    public class Item{
        public string WName{ get; set; }
        public string Rarity{ get; set; }

        public Item(string wName, string rarity){
            this.WName = wName;
            this.Rarity = rarity;
        }
    }
    class Program{
        static async Task Main(string[] args){
            var equippedItems = new Dictionary<string, Item>{
                ["Epic"] = new ("Holy Sword", "Epic"), 
                ["Rare"] = new("Shit Sword", "Rare")
            };

            var str = equippedItems.Where(item => item.Value.WName == "Holy Sword")
                .Select(x => x.Value.Rarity).First();

            var tmp = equippedItems[equippedItems
                .Where(x => x.Value.WName is "Holy Sword")
                .Select(y => y.Value.Rarity).First()];
            Console.WriteLine(tmp.WName);
            return;
            
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
                        break;
                    case "3":
                        playerList = await GetAllPlayers(player);
                        break;
                }
            }
        }

        static async Task<Player> GetPlayer(Player player){
            return await player.Get(Guid.Parse("5ae5e31c-ffd4-4c4e-847f-7d98f02a319c"));
        }

        static async Task<List<Player>> GetAllPlayers(Player player){
            var players = await player.GetAll();
            foreach (var getPlayer in players){
                Custom.WriteMultiLines(ConsoleColor.White,
                    $"Id: {getPlayer.Id}", $"Name: {getPlayer.Name}", $"Level: {getPlayer.Level}");
            }

            return players;
        }

        static async Task<Player> CreateCharacter(Player player){
            Custom.WriteLine("Character name:", ConsoleColor.Yellow);
            var userInput = Custom.ReadLine(ConsoleColor.Green);
            var createdPlayer = await player.Create(userInput);
            Custom.WriteMultiLines(ConsoleColor.White, "Created player:", $"Id: {createdPlayer.Id}",
                $"Name: {createdPlayer.Name}", $"Level: {createdPlayer.Level}");
            return createdPlayer;
        }
    }
}