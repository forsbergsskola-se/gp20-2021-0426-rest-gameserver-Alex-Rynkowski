using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Model;
using Newtonsoft.Json;

namespace Client.Requests{
    public class PlayerResponse{
        public static async Task<Player> Get(Guid playerId){
            var player = await RestApi.Api.GetResponse<Player>($"players/Get/{playerId}");
            return player;
        }

        public static async Task<Player> Get(string playerName){
            var player = await RestApi.Api.GetResponse<Player>($"players/Get/{playerName}");
            return player;
        }

        public static async Task<Player> Modify(Guid playerId, ModifiedPlayer modifiedPlayer){
            return await RestApi.Api.PutRequest<Player>($"players/{playerId}/modify",
                JsonConvert.SerializeObject(modifiedPlayer));
        }

        public static async Task<List<Player>> GetAll(){
            var players = await RestApi.Api.GetResponse<List<Player>>("/players");
            return players;
        }

        public static async Task<Player> Create(string name){
            return await RestApi.Api.PostRequest<Player>("players/create",
                JsonConvert.SerializeObject(new Player{Name = name}));
        }

        public async Task<Player> Delete(Guid id){
            var player = await RestApi.Api.GetResponse<Player>($"/Delete/{id}");
            return player;
        }
    }
}