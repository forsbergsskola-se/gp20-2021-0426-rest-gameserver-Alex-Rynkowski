using MongoDB.Bson.Serialization.Attributes;

namespace MMORPG.Data{
    public class Item{
        [BsonElement] public string ItemName{ get; set; }
        [BsonElement] public ItemTypes ItemType{ get; set; }
        [BsonElement] public int LevelRequirement{ get; set; }

        [BsonElement] public int LevelBonus{ get; set; }
        [BsonElement] public ItemRarity Rarity{ get; set; }
        [BsonElement] public bool IsDeleted{ get; set; }

        [BsonElement] public int SellValue{ get; set; }
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