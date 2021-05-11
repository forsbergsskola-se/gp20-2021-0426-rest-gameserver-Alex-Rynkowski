using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MMORPG.Items;
using MMORPG.Players;

namespace MMORPG.Api{
    public interface IRepository{
        Task<Player> Get(Guid id);
        Task<Player[]> GetAll();
        Task<Player> Create(string name);
        Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer);
        Task<Player> Delete(Guid id);
        
        public Task<Player> CreateItem(Guid id, string itemName, ItemTypes itemType);
        public Task<Item> DeleteItem(Guid id, string itemName);
        public Task<List<Item>> GetInventory(Guid id);
        public Task<Item> GetItem(Guid id,string name);

        public Task<Item> SellItem(Guid id, string itemName);
        
        public Task<Item> EquipSword(Guid id, string weaponName);
        public Task<Item> EquipShield(Guid id, string shieldName);
        public Task<Item> EquipArmor(Guid id, string armorName);
        public Task<Item> EquipHelmet(Guid id, string helmetName);
    }
}