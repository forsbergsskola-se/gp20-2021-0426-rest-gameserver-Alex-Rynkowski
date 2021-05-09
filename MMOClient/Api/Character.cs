using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Utilities;

namespace Client.Api{
    public class Character{
        public async Task<Player> Get(Guid id){
            var player = await ApiConnection.GetResponse<IPlayer>("/Get");
            return (Player) player;
        }

        public async Task<List<Player>> GetAll(){
            var players = await ApiConnection.GetResponse<List<Player>>("/GetAll");
            return players;
        }

        public async Task Create(string name){
            await ApiConnection.SendRequest($"/Create/{name}");
        }

        public async Task<Player> Delete(Guid id){
            var player = await ApiConnection.GetResponse<Player>($"/Delete/{id}");
            return player;
        }
    }
}