using System;
using System.Linq;
using MMORPG.Utilities;
using MongoDB.Bson.Serialization.Attributes;

namespace MMORPG.Items{
    [BsonNoId]
    public class Weapon : IItem{
        [BsonElement] public Guid Id{ get; set; }
        [BsonElement] public string Name{ get; set; }
        [BsonElement] public int LevelRequirement{ get; set; }
        [BsonElement] public int SellValue{ get; set; }
        [BsonElement] public int LevelBonus{ get; set; }
        [BsonElement] public bool IsDeleted{ get; set; }
        [BsonElement] public DateTime CreationTime{ get; set; }
        [BsonElement] public string Rarity{ get; set; }
        [BsonElement] public string Category => this.Name;
        [BsonElement] public string Type{ get; set; }

        public Weapon(string name, string type){
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Type = type;
            this.IsDeleted = false;
            this.CreationTime = DateTime.Now;
            this.LevelRequirement = new Random().Next(0, 10);
            var rarity =
                (ItemRarity) new Random().Next(0, (int) Enum.GetValues(typeof(ItemRarity)).Cast<ItemRarity>().Last() + 1);
            this.Rarity = rarity.ToString();
            this.SellValue = Calculate.CalculateItemSellValue(50, this.LevelRequirement, rarity + 1);
            Console.WriteLine($"Sell value: {this.SellValue}  Rarity: {rarity} Level requirement: {this.LevelRequirement}");
        }
    }
}