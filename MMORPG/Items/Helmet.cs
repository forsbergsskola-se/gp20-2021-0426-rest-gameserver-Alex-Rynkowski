using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MMORPG.Items{
    [BsonNoId]
    public class Helmet : Item{
        public override string ItemType => nameof(Helmet);
        public override string Category => "Plate Helmet";

        public Helmet(){
        }
    }
}