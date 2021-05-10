using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MMORPG.Items{
    [BsonNoId]
    public class Armor : Item{
        public override string ItemType => nameof(Armor);
        public override string Category => "Plate Armor";

        public Armor(){
        }
    }
}