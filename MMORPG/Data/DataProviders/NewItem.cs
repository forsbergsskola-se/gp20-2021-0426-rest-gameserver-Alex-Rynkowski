using System;
using System.Threading.Tasks;
using MMORPG.Api;
using MMORPG.BLL;
using MongoDB.Driver;

namespace MMORPG.Data{
    public class NewItem{
        readonly Item item;
        readonly IPlayerRepository playerRepository;

        public NewItem(string itemName, ItemTypes itemType, int levelRequirement, int levelBonus, ItemRarity rarity,
            int sellValue){
            this.item = new Item{
                ItemName = itemName,
                ItemType = itemType,
                LevelRequirement = levelRequirement,
                LevelBonus = levelBonus,
                Rarity = rarity,
                SellValue = sellValue,
                IsDeleted = false
            };
            this.playerRepository = new MongoDbPlayerRepository();
        }

        public async Task<Item> CreateItem(Guid playerId){
            var update = Builders<Player>.Update.Push(x => x.Inventory, this.item);
            await this.playerRepository.UpdatePlayer(playerId, update);
            return this.item;
        }
    }
}