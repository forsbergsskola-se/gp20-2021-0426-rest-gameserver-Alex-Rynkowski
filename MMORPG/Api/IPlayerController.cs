using System;
using System.Threading.Tasks;
using MMORPG.Items;
using MMORPG.Players;

namespace MMORPG.Api{
    public interface IPlayerController{
        Task<Player> Get(Guid id);
        Task<Player[]> GetAll();
        Task<Player> Create(string name);
        Task<Player> Modify(Guid id, ModifiedPlayer player);
        Task<Player> Delete(Guid id);
    }
}