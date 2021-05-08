using System;
using System.IO;
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

        public Task<Player[]> GetAll(){
            throw new NotImplementedException();
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

        public Task<Player> Delete(Guid id){
            throw new NotImplementedException();
        }
    }
}