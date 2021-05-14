using System;
using System.Linq;
using System.Threading.Tasks;
using MMORPG.Database;
using MMORPG.Exceptions;
using MMORPG.Utilities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MMORPG.Api{
    public class MongoDbPlayerRepository : IPlayerRepository{
        public async Task<Player> Get(Guid id){
            var foundPlayer = await ApiUtility.GetPlayerCollection().Find(id.GetPlayerById()).SingleAsync();
            if (!foundPlayer.IsDeleted) return foundPlayer;

            throw new NotFoundException("Player does not exist or has been deleted");
        }

        public async Task<Player> GetPlayerByName(string name){
            var match = new BsonDocument{
                {
                    "$match", new BsonDocument{
                        {"Name", name}
                    }
                }
            };
            var pipeline = new[]{match};
            var playerAgg = await ApiUtility.GetPlayerCollection().AggregateAsync<Player>(pipeline);
            return playerAgg.ToList().First();
        }

        public async Task<Player[]> GetAll(){
            var allPlayers = await ApiUtility.GetPlayerCollection().Find(_ => true).ToListAsync();
            return allPlayers.Select(player => player)
                .Where(playerDe => !playerDe.IsDeleted).ToArray();
        }

        public async Task<Player> Create(string name){
            var exists = await ApiUtility.GetPlayerCollection().Find(_ => _.Name == name).AnyAsync();
            if (exists)
                throw new Exception("Player with that name already exists");

            var player = new NewPlayer(name).SetupNewPlayer(new Player());
            await SendPlayerDataToMongo(player);
            return player;
        }

        async Task SendPlayerDataToMongo(Player player){
            await ApiUtility.GetPlayerCollection().InsertOneAsync(player);
        }

        public async Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer){
            var update = Builders<Player>.Update
                .Set(x => x.Gold, modifiedPlayer.Gold)
                .Set(x => x.Score, modifiedPlayer.Score)
                .Set(x => x.Level, modifiedPlayer.Level);

            return await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(id.GetPlayerById(), update,
                new FindOneAndUpdateOptions<Player>{
                    ReturnDocument = ReturnDocument.After
                });
        }


        public async Task<Player> Delete(Guid id){
            var update = Builders<Player>.Update.Set("IsDeleted", true);
            return await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(id.GetPlayerById(), update,
                new FindOneAndUpdateOptions<Player>{
                    ReturnDocument = ReturnDocument.After
                });
        }

        public async Task<Player> PurchaseLevel(Guid id){
            var player = await Get(id);
            if (player.CurrentExperience < player.ExperienceToNextLevel)
                throw new Exception("Not enough experience");
            if (!Calculate.CanAffordLevel(player.Level, player.Gold))
                throw new Exception("Not enough gold");

            player.Gold -= (player.Level + 1) * 100;
            player.Level++;
            var update = Builders<Player>.Update
                .Inc(l => l.Level, player.Level)
                .Set(g => g.Gold, player.Gold)
                .Set(e => e.CurrentExperience, 0)
                .Set(e => e.CurrentExperience, player.Level + 1 * 100);
            return await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(id.GetPlayerById(), update,
                new FindOneAndUpdateOptions<Player>{
                    ReturnDocument = ReturnDocument.After
                });
        }
    }
}