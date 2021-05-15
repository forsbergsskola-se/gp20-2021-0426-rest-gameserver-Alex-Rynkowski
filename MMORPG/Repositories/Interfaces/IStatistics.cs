using System.Threading.Tasks;
using MMORPG.Data;

namespace MMORPG.Repositories{
    public interface IStatistics{
        public Task<PlayerStatistics> GetAllPlayers();
    }
}