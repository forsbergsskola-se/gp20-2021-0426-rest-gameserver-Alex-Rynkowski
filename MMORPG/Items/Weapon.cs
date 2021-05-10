using MongoDB.Bson.Serialization.Attributes;

namespace MMORPG.Items{
    [BsonNoId]
    public class Weapon : Item{
        public override string ItemType => nameof(Weapon);
        public override string Category => "Sword";

        public Weapon(){
        }
    }
}