using System;
using System.Threading.Tasks;
using MMORPG.Items;
using MMORPG.Players;

namespace MMORPG.Database{
    public interface IEquip{
        public Task<Item> EquipSword(Guid id, string weaponName);
        public Task<Item> EquipShield(Guid id, string shieldName);
        public Task<Item> EquipArmor(Guid id, string armorName);
        public Task<Item> EquipHelmet(Guid id, string helmetName);
    }
}