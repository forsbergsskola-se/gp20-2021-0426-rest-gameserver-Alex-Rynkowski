using System;
using System.Linq;
using System.Threading.Tasks;
using MMORPG.Data;
using MMORPG.Exceptions;
using MMORPG.Utilities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MMORPG.Repositories{
    public class MongoDbPlayerRepository : IPlayerRepository{
        /// <summary>
        /// FindOneAndUpdateAsync (player)
        /// </summary>
        static IRepository Repository => new MongoDbRepository();

        public async Task<Player> UpdatePlayer(Guid playerId, UpdateDefinition<Player> update){
            return await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(playerId.GetPlayerById(), update,
                new FindOneAndUpdateOptions<Player>{
                    ReturnDocument = ReturnDocument.After,
                    IsUpsert = true
                });
        }

        public async Task<Player> Get(Guid id){
            var foundPlayer = await ApiUtility.GetPlayerCollection().Find(id.GetPlayerById()).SingleAsync();
            if (!foundPlayer.IsDeleted)
                return await Repository.QuestRepository.AssignQuests(foundPlayer, foundPlayer.LastLogin);

            throw new PlayerException("Player does not exist or has been deleted");
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
            var player = await playerAgg.FirstAsync();
            return await Repository.QuestRepository.AssignQuests(player, player.LastLogin);
        }

        public async Task<Player[]> GetAll(){
            var allPlayers = await ApiUtility.GetPlayerCollection().Find(_ => true).ToListAsync();
            return allPlayers.Select(player => player)
                .Where(playerDe => !playerDe.IsDeleted).ToArray();
        }

        public async Task<Player> Create(NewPlayer newPlayer){
            var exists = await ApiUtility.GetPlayerCollection().Find(_ => _.Name == newPlayer.Name).AnyAsync();
            if (exists)
                throw new PlayerException("Player with that name already exists");

            return await new NewPlayer(newPlayer.Name).SetupNewPlayer(new Player());
        }

        public async Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer){
            var update = Builders<Player>.Update
                .Set(x => x.Gold, modifiedPlayer.Gold)
                .Set(x => x.Score, modifiedPlayer.Score)
                .Set(x => x.Level, modifiedPlayer.Level)
                .Set(x => x.ExperienceToNextLevel, modifiedPlayer.Level * 100);

            return await UpdatePlayer(id, update);
        }

        public async Task<Player> Delete(Guid playerId){
            var update = Builders<Player>.Update.Set("IsDeleted", true);
            return await UpdatePlayer(playerId, update);
        }

        public async Task<Player> LevelUp(Guid playerId){
            var player = await Get(playerId);

            if (player.CurrentExperience < player.ExperienceToNextLevel)
                throw new PlayerException("Not enough experience");

            if (!Calculate.CanAffordLevel(player.Level, player.Gold))
                throw new PlayerException("Not enough gold");

            player.Gold -= (player.Level + 1) * 100;
            player.Level++;
            var update = Builders<Player>.Update
                .Set(l => l.Level, player.Level)
                .Set(g => g.Gold, player.Gold)
                .Set(e => e.CurrentExperience, 0)
                .Set(e => e.CurrentExperience, player.Level + 1 * 100);
            return await UpdatePlayer(playerId, update);
        }
    }
}