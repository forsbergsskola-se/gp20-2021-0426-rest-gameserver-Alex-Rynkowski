using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMORPG{
    public interface IRepository{
        Task<Player> Get(Guid id);
        Task<List<Player>> GetAll();
        Task<Player> Create(Player player);
        Task<Player> Modify(Guid id, ModifiedPlayer player);
        Task<Player> Delete(Guid id);
    }
}