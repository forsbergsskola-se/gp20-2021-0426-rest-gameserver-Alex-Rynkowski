using System;
using System.Threading.Tasks;
using MMORPG.Database;
using MMORPG.Exceptions;
using MMORPG.Utilities;
using MongoDB.Driver;

namespace MMORPG.Api{
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

            await UnEquip(id, name);
            var update = Builders<Player>.Update
                .Set(x => x.EquippedItems[item.ItemType.ToString()], item)
                .Inc(x => x.Level, item.LevelBonus);
            await ApiUtility.GetPlayerCollection()
                .UpdateOneAsync(id.GetPlayerById(), update, new UpdateOptions{IsUpsert = true});
            return item;
        }

        async Task UnEquip(Guid id, string name){
            var item = await ItemRepository.GetItem(id, name);
            var update = Builders<Player>.Update
                .Set(x => x.EquippedItems[item.ItemType.ToString()], null)
                .Inc(player => player.Level, -item.LevelBonus);

            await ApiUtility.GetPlayerCollection().UpdateOneAsync(id.GetPlayerById(), update);
        }

        static bool IsNullOrWrongType(ItemTypes type, Item item){
            return item == null || item.ItemType != type;
        }
    }
}