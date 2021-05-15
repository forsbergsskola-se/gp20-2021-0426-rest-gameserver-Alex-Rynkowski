using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Utilities;
using Newtonsoft.Json;

namespace Client.Model{
    public class Player{
        public Guid Id{ get; set; }
        public string Name{ get; set; }
        public int Gold{ get; set; }
        public int Score{ get; set; }
        public int Level{ get; set; }
        public bool IsDeleted{ get; set; }
        public DateTime CreationTime{ get; set; }
        public int CurrentExperience{ get; set; }
        public int ExperienceToNextLevel{ get; set; }
        public List<Item> InventoryList{ get; set; }

        public static async Task<Player> Get(Guid id){
            var player = await ApiConnection.GetResponse<Player>($"players/Get/{id}");
            return player;
        }

        public static async Task<Player> Get(string id){
            var player = await ApiConnection.GetResponse<Player>($"players/Get/{id}");
            return player;
        }

        public static async Task<List<Player>> GetAll(){
            var players = await ApiConnection.GetResponse<List<Player>>("/players");
            return players;
        }

        public static async Task<Player> Create(string name){
            return await ApiConnection.SendRequest<Player>("players/create",
                JsonConvert.SerializeObject(new Player{Name = name}));
        }

        public async Task<Player> Delete(Guid id){
            var player = await ApiConnection.GetResponse<Player>($"/Delete/{id}");
            return player;
        }
    }
}