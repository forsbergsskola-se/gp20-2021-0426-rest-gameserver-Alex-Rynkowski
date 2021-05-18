using System;
using System.Threading.Tasks;
using MMORPG.Data;

namespace MMORPG.Repositories{
    public interface IEquipRepository{
        Task<Item> EquipSword(Guid id, Item weaponName);
        Task<Item> EquipShield(Guid id, Item shieldName);
        Task<Item> EquipArmor(Guid id, Item armorName);
        Task<Item> EquipHelmet(Guid id, Item helmetName);
        public Task UnEquip(Guid playerId, Item item);
    }
}