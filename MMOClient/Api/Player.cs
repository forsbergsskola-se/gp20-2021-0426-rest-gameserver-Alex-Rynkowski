using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Utilities;

namespace Client.Api{
    public class Player : IPlayer{
        public Guid Id{ get; set; }
        public string Name{ get; set; }
        public int Score{ get; set; }
        public int Level{ get; set; }
        public bool IsDeleted{ get; set; }
        public DateTime CreationTime{ get; set; }
        public int CurrentExperience{ get; set; }
        public int ExperienceToNextLevel{ get; set; }
        public List<Item> InventoryList{ get; set; }

        public async Task<Player> Get(Guid id){
            var player = await ApiConnection.GetResponse<Player>("/Get");
            return player;
        }

        public async Task<List<Player>> GetAll(){
            var players = await ApiConnection.GetResponse<List<Player>>("/GetAll");
            return players;
        }

        public async Task<Player> Create(string name){
            return await ApiConnection.SendRequest<Player>($"/Create/{name}");
        }

        public async Task<Player> Delete(Guid id){
            var player = await ApiConnection.GetResponse<Player>($"/Delete/{id}");
            return player;
        }
    }
}