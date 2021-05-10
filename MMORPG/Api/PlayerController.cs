using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Exceptions;
using MMORPG.Players;
using MMORPG.Utilities;
using MongoDB.Driver;

namespace MMORPG.Api{
    [ApiController]
    [Route("players")]
    public class PlayerController : IPlayerController{
        [HttpGet("Get/")]
        public async Task<Player> Get(Guid id){
            Player player;
            var filter = Builders<Player>.Filter.Eq(nameof(player.Id), id.ToString());
            var foundPlayer = await ApiUtility.GetPlayerCollection().Find(filter).SingleAsync();
            //player = BsonSerializer.Deserialize<Player>(foundPlayer);

            if (!foundPlayer.IsDeleted) return foundPlayer;

            throw new NotFoundException("Player does not exist or has been deleted");
        }

        [HttpGet("GetAll")]
        public async Task<Player[]> GetAll(){
            var allPlayers = await ApiUtility.GetPlayerCollection().Find(_ => true).ToListAsync();
            return allPlayers.Select(player => player)
                .Where(playerDe => !playerDe.IsDeleted).ToArray();
        }

        [HttpPost("Create/")]
        public async Task<Player> Create(string name){
            Console.WriteLine("Sending created player to client");
            var player = new NewPlayer(name).SetupNewPlayer(new Player());
            Console.WriteLine(player.Id);
            await SendPlayerDataToMongo(player);
            return player;
        }

        async Task SendPlayerDataToMongo(Player player){
            await ApiUtility.GetPlayerCollection().InsertOneAsync(player);
        }

        [HttpPost("Modify/")]
        public Task<Player> Modify(Guid id, ModifiedPlayer player){
            throw new NotImplementedException();
        }


        [HttpDelete("Delete/")]
        public async Task<Player> Delete(Guid id){
            var filter = Builders<Player>.Filter.Eq(nameof(Player.Id), id.ToString());
            var update = Builders<Player>.Update.Set("IsDeleted", true);
            await ApiUtility.GetPlayerCollection().UpdateOneAsync(filter, update, new UpdateOptions{IsUpsert = false});
            var player = await Get(id);
            return player;
        }
    }
}