using System;
using System.Threading.Tasks;
using MMORPG.Data;
using MongoDB.Driver;

namespace MMORPG.Repositories{
    public interface IPlayerRepository{
        Task<Player> UpdatePlayer(Guid playerId, UpdateDefinition<Player> update);
        Task<Player> Get(Guid id);
        Task<Player> GetPlayerByName(string name);
        Task<Player[]> GetAll();
        Task<Player> Create(NewPlayer name);
        Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer);
        Task<Player> Delete(Guid id);
        Task<Player> LevelUp(Guid playerId);
    }
}