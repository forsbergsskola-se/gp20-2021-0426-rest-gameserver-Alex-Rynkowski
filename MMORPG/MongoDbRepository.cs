using System;
using System.IO;
using System.Threading.Tasks;
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
            var newPlayer = player;
            newPlayer.Name = "Alex Holmes";
            var json = JsonConvert.SerializeObject(newPlayer);
            var doc = BsonDocument.Parse(json);
            await this.collection.InsertOneAsync(doc);
            return newPlayer;
        }

        public Task<Player> Modify(Guid id, ModifiedPlayer player){
            throw new NotImplementedException();
        }

        public Task<Player> Delete(Guid id){
            throw new NotImplementedException();
        }
    }
}