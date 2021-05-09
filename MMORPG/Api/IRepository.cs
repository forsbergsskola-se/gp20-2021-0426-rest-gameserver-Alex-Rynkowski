using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMORPG.Api{
    public interface IRepository{
        Task<Player> Get(Guid id);
        Task<List<Player>> GetAll();
        Task<Player> Create(string name);
        Task<Player> Modify(Guid id, ModifiedPlayer player);
        Task<Player> Delete(Guid id);
    }
}