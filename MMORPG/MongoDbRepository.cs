using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MMORPG.Utilities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace MMORPG{
    //Your data should follow this format:
    //* You can name your database to game
    //* Players should be stored in a collection called players
    //* Items should be stored in a list inside the Player document
    public class MongoDbRepository : IRepository{
        IMongoCollection<BsonDocument> collection;

        public MongoDbRepository(){
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("game");
            this.collection = database.GetCollection<BsonDocument>("Players");
        }

        public Task<Player> Get(Guid id){
            throw new NotImplementedException();
        }

        public async Task<List<Player>> GetAll(){
            var allPlayers = await this.collection.Find(_ => true).ToListAsync();
            return allPlayers.Select(t => BsonSerializer.Deserialize<Player>(t)).ToList();
        }

        public async Task<Player> Create(Player player){
            Custom.WriteLine("Enter character name:", ConsoleColor.Yellow);
            player.Name = Custom.ReadLine(ConsoleColor.DarkGreen);
            SetupPlayerStandardValues(player);
            await SendPlayerDataToMongo(player);
            Custom.WriteLine($"Character \"{player.Name}\" created with id \"{player.Id}\".", ConsoleColor.White);
            return player;
        }

        async Task SendPlayerDataToMongo(Player player){
            var serializedPlayer = JsonConvert.SerializeObject(player);
            var bsonDocument = BsonDocument.Parse(serializedPlayer);
            await this.collection.InsertOneAsync(bsonDocument);
        }

        static void SetupPlayerStandardValues(Player player){
            player.Id = Guid.NewGuid();
            player.Score = 0;
            player.Level = 1;
            player.IsDeleted = false;
            player.CreationTime = DateTime.Now;
        }

        public Task<Player> Modify(Guid id, ModifiedPlayer player){
            throw new NotImplementedException();
        }


        public async Task<Player> Delete(Guid id){
            Player player;
            var filter = Builders<BsonDocument>.Filter.Eq(nameof(player.Id), id.ToString());
            var foundPlayer = await this.collection.Find(filter).SingleAsync();
            //await this.collection.DeleteOneAsync(filter);
            player = BsonSerializer.Deserialize<Player>(foundPlayer);
            player.IsDeleted = true;
            Custom.WriteLine($"\"{player.Name}\" at level \"{player.Level}\"\nIs deleted: {player.IsDeleted} ",
                ConsoleColor.White);
            return player;
        }
    }
}