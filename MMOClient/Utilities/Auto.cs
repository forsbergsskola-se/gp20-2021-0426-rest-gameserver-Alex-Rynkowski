using System;
using System.Threading.Tasks;
using Client.Model;
using Client.RestApi;

namespace Client.Utilities{
    public static class Auto{
        public static async void GenerateTenCharacters(){
            var names = new string[]{
                "Alex", "Marc", "Jack", "Anton", "David",
                "Johan", "Marcus", "Clara", "Sophie", "Linus"
            };
            var player = new Player();
            for (var i = 0; i < names.Length; i++){
                player = await CreateCharacter(player, names[i]);
            }
        }

        static async Task<Player> CreateCharacter(Player player, string name){
            var createdPlayer = await PlayerRequest.Create(name);
            Custom.WriteMultiLines(ConsoleColor.White, "Created player:", $"Id: {createdPlayer.Id}",
                $"Name: {createdPlayer.Name}", $"Level: {createdPlayer.Level}");
            return createdPlayer;
        }
    }
}