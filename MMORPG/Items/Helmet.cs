using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MMORPG.Items{
    [BsonNoId]
    public class Helmet : IItem{
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
    }
}