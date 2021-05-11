using System;
using System.Threading.Tasks;
using MMORPG.Api;
using MMORPG.Exceptions;
using MMORPG.Items;
using MMORPG.Players;
using MMORPG.Utilities;
using MongoDB.Driver;

namespace MMORPG.Database{
    public class EquipItem : IEquip{
        readonly IItemRepository itemRepository;
        readonly IRepository repository;

        public EquipItem(){
            this.repository = new MongoDbRepository();
            this.itemRepository = new ItemRepository();
        }

        public async Task<Item> EquipSword(Guid id, string weaponName)
            => await Equip(id, weaponName, ItemTypes.Sword);

        public async Task<Item> EquipShield(Guid id, string shieldName)
            => await Equip(id, shieldName, ItemTypes.Shield);

        public async Task<Item> EquipArmor(Guid id, string armorName)
            => await Equip(id, armorName, ItemTypes.Armor);

        public async Task<Item> EquipHelmet(Guid id, string helmetName)
            => await Equip(id, helmetName, ItemTypes.Helmet);


        async Task<Item> Equip(Guid id, string name, ItemTypes type){
            var item = await this.itemRepository.GetItem(id, name);
            if (IsNullOrWrongType(type, item))
                throw new NotFoundException("Item not found in player inventory");

            var player = await this.repository.Get(id);
            //TODO implement custom exception
            if (player.Level < item.LevelRequirement)
                throw new Exception("Level not high enough");

            var update = Builders<Player>.Update.Set(x => x.EquippedItems[item.ItemType], item);
            await ApiUtility.GetPlayerCollection()
                .UpdateOneAsync(Db.GetPlayerById(id), update, new UpdateOptions{IsUpsert = true});
            return item;
        }

        static bool IsNullOrWrongType(ItemTypes type, Item item){
            return item == null || item.ItemType != type.ToString();
        }
    }
}