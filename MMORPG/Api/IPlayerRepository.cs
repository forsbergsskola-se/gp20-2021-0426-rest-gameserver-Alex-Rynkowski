using System;
using System.Threading.Tasks;
using MMORPG.Database;

namespace MMORPG.Api{
    public interface IPlayerRepository{
        Task<Player> Get(Guid id);
        Task<Player> GetPlayerByName(string name);
        Task<Player[]> GetAll();
        Task<Player> Create(string name);
        Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer);
        Task<Player> Delete(Guid id);
        Task<Player> PurchaseLevel(Guid id);
    }
}