using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Client.Api{
    public class Item{
        public Guid Id{ get; set; }
        public string ItemName{ get; set; }
        public int LevelRequirement{ get; set; }
        public int SellValue{ get; private set; }
        public int LevelBonus{ get; set; }
        public bool IsDeleted{ get; set; }
        public DateTime CreationTime{ get; set; }
        public string Rarity{ get; set; }
        public virtual string Category => this.ItemName;
        public virtual string ItemType{ get; set; }
    }
}