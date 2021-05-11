using System;
using System.Linq;
using MMORPG.Utilities;
using MongoDB.Bson.Serialization.Attributes;

namespace MMORPG.Database{
    public class Item{
        [BsonElement] public string ItemName{ get; set; }
        [BsonElement] public string ItemType{ get; set; }
        [BsonElement] public int LevelRequirement{ get; set; }

        [BsonElement] public int LevelBonus{ get; set; }
        [BsonElement] public string Rarity{ get; set; }
        [BsonElement] public bool IsDeleted{ get; set; }

        [BsonElement] public int SellValue{ get; private set; }

        public Item(string itemName, string itemType){
            this.ItemName = itemName;
            this.ItemType = itemType;
            SetupDefaults();
        }

        void SetupDefaults(){
            this.IsDeleted = false;
            this.LevelRequirement = new Random().Next(0, 10);
            var rarity =
                (ItemRarity) new Random().Next(0,
                    (int) Enum.GetValues(typeof(ItemRarity)).Cast<ItemRarity>().Last() + 1);
            this.Rarity = rarity.ToString();
            this.SellValue = Calculate.CalculateItemSellValue(50, this.LevelRequirement, rarity + 1);
            this.LevelBonus = (int) rarity;
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