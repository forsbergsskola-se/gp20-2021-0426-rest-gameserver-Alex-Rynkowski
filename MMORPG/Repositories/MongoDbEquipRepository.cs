using System;
using System.Threading.Tasks;
using MMORPG.Data;
using MMORPG.Exceptions;
using MMORPG.Utilities;
using MongoDB.Driver;

namespace MMORPG.Repositories{
    public class MongoDbEquipRepository : IEquipRepository{
        static IItemRepository ItemRepository => new MongoDbItemRepository();
        static IPlayerRepository PlayerRepository => new MongoDbPlayerRepository();

        public async Task<Item> EquipSword(Guid id, string weaponName)
            => await Equip(id, weaponName, ItemTypes.Sword);

        public async Task<Item> EquipShield(Guid id, string shieldName)
            => await Equip(id, shieldName, ItemTypes.Shield);

        public async Task<Item> EquipArmor(Guid id, string armorName)
            => await Equip(id, armorName, ItemTypes.Armor);

        public async Task<Item> EquipHelmet(Guid id, string helmetName)
            => await Equip(id, helmetName, ItemTypes.Helmet);

        async Task<Item> Equip(Guid id, string name, ItemTypes type){
            var item = await ItemRepository.GetItem(id, name);
            if (IsNullOrWrongType(type, item))
                throw new NotFoundException("Item not found in player inventory");

            var player = await PlayerRepository.Get(id);

            if (player.Level < item.LevelRequirement)
                throw new PlayerException("Level not high enough");

            await UnEquip(id, item);
            var update = Builders<Player>.Update
                .Set(x => x.EquippedItems[item.ItemType.ToString()], item)
                .Inc(x => x.Level, item.LevelBonus);
            await ApiUtility.GetPlayerCollection()
                .UpdateOneAsync(id.GetPlayerById(), update, new UpdateOptions{IsUpsert = true});
            return item;
        }

        public async Task UnEquip(Guid playerId, Item item){
            var equippedItems = await ApiUtility.GetPlayerCollection()
                .FindAsync(x => x.EquippedItems[item.ItemType.ToString()] == item);

            if (equippedItems != null)
                return;

            var getItem = await ItemRepository.GetItem(playerId, item.ItemName);
            var update = Builders<Player>.Update
                .Set(x => x.EquippedItems[getItem.ItemType.ToString()], null)
                .Inc(player => player.Level, -getItem.LevelBonus);

            await ApiUtility.GetPlayerCollection().UpdateOneAsync(playerId.GetPlayerById(), update);
        }

        static bool IsNullOrWrongType(ItemTypes type, Item item){
            return item == null || item.ItemType != type;
        }
    }
}