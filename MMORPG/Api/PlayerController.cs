using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Exceptions;
using MMORPG.Players;
using MMORPG.Utilities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace MMORPG.Api{
    [ApiController]
    [Route("players")]
    public class PlayerController : IPlayerController{
        readonly IMongoCollection<BsonDocument> collection;

        public PlayerController(){
            this.collection = ApiUtility.GetDatabase().GetCollection<BsonDocument>("Players");
        }

        [HttpGet("Get/{id:guid}")]
        public async Task<Player> Get(Guid id){
            Player player;
            var filter = Builders<BsonDocument>.Filter.Eq(nameof(player.Id), id.ToString());
            var foundPlayer = await this.collection.Find(filter).SingleAsync();
            player = BsonSerializer.Deserialize<Player>(foundPlayer);

            if (!player.IsDeleted) return player;

            throw new NotFoundException("Player does not exist or has been deleted");
        }

        [HttpGet("GetAll")]
        public async Task<Player[]> GetAll(){
            var allPlayers = await this.collection.Find(_ => true).ToListAsync();
            return allPlayers.Select(player => BsonSerializer.Deserialize<Player>(player))
                .Where(playerDe => !playerDe.IsDeleted).ToArray();
        }

        [HttpPost("Create/{name}")]
        public async Task<Player> Create(string name){
            Console.WriteLine("Sending created player to client");
            var player = new Player();
            var newPlayer = new NewPlayer(name);
            newPlayer.SetupNewPlayer(player);
            await SendPlayerDataToMongo(player);
            return player;
        }

        async Task SendPlayerDataToMongo(Player player){
            var serializedPlayer = JsonConvert.SerializeObject(player);
            var bsonDocument = BsonDocument.Parse(serializedPlayer);
            await this.collection.InsertOneAsync(bsonDocument);
        }

        [HttpPost("Modify/{id:guid}")]
        public Task<Player> Modify(Guid id, ModifiedPlayer player){
            throw new NotImplementedException();
        }


        [HttpDelete("Delete/{id:guid}")]
        public async Task<Player> Delete(Guid id){
            var filter = Builders<BsonDocument>.Filter.Eq(nameof(Player.Id), id.ToString());
            var update = Builders<BsonDocument>.Update.Set("IsDeleted", true);
            await this.collection.UpdateOneAsync(filter, update, new UpdateOptions{IsUpsert = false});
            var player = await Get(id);
            return player;
        }
        //players/alex/inventory
        [HttpGet]
        public Task<Inventory> GetInventory(Guid id){
            throw new NotImplementedException();
        }
    }
}