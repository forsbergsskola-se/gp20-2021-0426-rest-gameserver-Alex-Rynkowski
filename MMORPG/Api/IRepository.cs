using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MMORPG.Database;

namespace MMORPG.Api{
    public interface IRepository{
        Task<Player> Get(Guid id);
        Task<Player> GetPlayerByName(string name);
        Task<Player[]> GetAll();
        Task<Player> Create(string name);
        Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer);
        Task<Player> Delete(Guid id);
        Task<Player> PurchaseLevel(Guid id);

        Task<Player> CreateItem(Guid id, string itemName, ItemTypes itemType);
        Task<Item> DeleteItem(Guid id, string itemName);
        Task<List<Item>> GetInventory(Guid id);
        Task<Item> GetItem(Guid id, string name);
        Task<Item> SellItem(Guid id, string itemName);
        
        Task<Item> EquipSword(Guid id, string weaponName);
        Task<Item> EquipShield(Guid id, string shieldName);
        Task<Item> EquipArmor(Guid id, string armorName);
        Task<Item> EquipHelmet(Guid id, string helmetName);

        Task<Quest> CreateQuest(string questName, int levelRequirement);
        Task<Quest> GetQuest(Guid id);
        Task<Quest[]> GetAllQuests();
        void AssignQuestInterval();
    }
}