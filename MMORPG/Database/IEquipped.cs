using System.Threading.Tasks;
using MMORPG.Players;

namespace MMORPG.Database{
    public interface IEquipped{
        public Task<Player> WeaponEquipped();
    }
}