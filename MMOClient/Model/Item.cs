using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Api;
using Client.Utilities;
using Newtonsoft.Json;

namespace Client.Model{
    public class Item{
        public string ItemName{ get; set; }
        public ItemTypes ItemType{ get; set; }
        public int LevelRequirement{ get; set; }
        public int LevelBonus{ get; set; }
        public ItemRarity Rarity{ get; set; }
        public bool IsDeleted{ get; set; }
        public int SellValue{ get; set; }

        public static async Task<Item> CreateItem(Guid playerId, Item item){
            return await ApiConnection.SendRequest<Item>($"/players/{playerId}/items/create",
                JsonConvert.SerializeObject(item));
        }

        public static async Task<List<Item>> GetAll(Guid playerId)
            => await ApiConnection.GetResponse<List<Item>>($"players/{playerId}/items");

        public static async Task<Item> Get(Guid playerId, string itemName)
            => await ApiConnection.GetResponse<Item>($"players/{playerId}/items/get/{itemName}");

        public static async Task<Item> Sell(Guid playerId, string itemName)
            => await ApiConnection.DeleteRequest<Item>($"players/{playerId}/items/{itemName}");
    }

    public enum ItemRarity{
        Common,
        Uncommon,
        Rare,
        Epic
    }

    public enum ItemTypes{
        Sword,
        Shield,
        Armor,
        Helmet,
        Potion,
    }
}