using System;
using System.Threading.Tasks;
using MMORPG.Database;

namespace MMORPG.Api{
    public interface IEquipRepository{
        Task<Item> EquipSword(Guid id, string weaponName);
        Task<Item> EquipShield(Guid id, string shieldName);
        Task<Item> EquipArmor(Guid id, string armorName);
        Task<Item> EquipHelmet(Guid id, string helmetName);
    }
}