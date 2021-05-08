using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MMORPG.Utilities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace MMORPG{
    //Your data should follow this format:
    //* You can name your database to game
    //* Players should be stored in a collection called players
    //* Items should be stored in a list inside the Player document
    public class MongoDbRepository : IRepository{
        readonly IMongoCollection<BsonDocument> collection;

        public MongoDbRepository(){
            this.collection = DatabaseConnection.GetDatabase().GetCollection<BsonDocument>("Players");
        }

        public async Task<Player> Get(Guid id){
            Player player;
            var filter = Builders<BsonDocument>.Filter.Eq(nameof(player.Id), id.ToString());
            var foundPlayer = await this.collection.Find(filter).SingleAsync();
            player = BsonSerializer.Deserialize<Player>(foundPlayer);
            return player;
        }

        public async Task<List<Player>> GetAll(){
            var allPlayers = await this.collection.Find(_ => true).ToListAsync();
            return allPlayers.Select(player => BsonSerializer.Deserialize<Player>(player))
                .Where(playerDe => !playerDe.IsDeleted).ToList();
        }

        public async Task<Player> Create(Player player){
            Custom.WriteLine("Enter character name:", ConsoleColor.Yellow);
            var newPlayer = new NewPlayer(Custom.ReadLine(ConsoleColor.DarkGreen));
            newPlayer.SetupNewPlayer(player);
            await SendPlayerDataToMongo(player);
            PrintOutInfo(player);
            return player;
        }

        static void PrintOutInfo(Player player){
            Custom.MultiWriteLine(ConsoleColor.White,
                $"Id: {player.Id}", $"Name: {player.Name}", $"Level: {player.Level}",
                $"Current exp: {player.CurrentExperience}", $"Exp to next level: {player.ExperienceToNextLevel}",
                $"Created: {player.CreationTime}");
        }

        async Task SendPlayerDataToMongo(Player player){
            var serializedPlayer = JsonConvert.SerializeObject(player);
            var bsonDocument = BsonDocument.Parse(serializedPlayer);
            await this.collection.InsertOneAsync(bsonDocument);
        }

        public Task<Player> Modify(Guid id, ModifiedPlayer player){
            throw new NotImplementedException();
        }


        public async Task<Player> Delete(Guid id){
            var filter = Builders<BsonDocument>.Filter.Eq(nameof(Player.Id), id.ToString());
            var update = Builders<BsonDocument>.Update.Set("IsDeleted", true);
            await this.collection.UpdateOneAsync(filter, update, new UpdateOptions{IsUpsert = false});
            var player = await Get(id);
            Custom.WriteLine($"\"{player.Name}\" at level \"{player.Level}\"\nIs deleted: {player.IsDeleted} ",
                ConsoleColor.White);
            return player;
        }
    }
}