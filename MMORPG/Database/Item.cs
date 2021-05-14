using System;
using System.Linq;
using MMORPG.Utilities;
using MongoDB.Bson.Serialization.Attributes;

namespace MMORPG.Database{
    public class Item{
        [BsonElement] public string ItemName{ get; set; }
        [BsonElement] public ItemTypes ItemType{ get; set; }
        [BsonElement] public int LevelRequirement{ get; set; }

        [BsonElement] public int LevelBonus{ get; set; }
        [BsonElement] public ItemRarity Rarity{ get; set; }
        [BsonElement] public bool IsDeleted{ get; set; }

        [BsonElement] public int SellValue{ get; private set; }

        public Item(string itemName, ItemTypes itemType, int levelRequirement, int levelBonus, ItemRarity rarity,
            int sellValue){
            this.ItemName = itemName;
            this.ItemType = itemType;
            this.LevelRequirement = levelRequirement;
            this.LevelBonus = levelBonus;
            this.Rarity = rarity;
            this.SellValue = sellValue;
            this.IsDeleted = false;
        }
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