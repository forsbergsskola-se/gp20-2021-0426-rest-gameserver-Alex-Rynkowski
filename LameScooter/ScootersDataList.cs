using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace LameScooter{
    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class ScootersDataList : IScootersDataList{
        public string Id{ get; set; }
        public string Name{ get; set; }
        public float X{ get; set; }
        public float Y{ get; set; }
        public int BikesAvailable{ get; set; }
        public int SpacesAvailable{ get; set; }
        public int Capacity{ get; set; }
        public bool AllowDropoff{ get; set; }
        public bool AllowOverloading{ get; set; }
        public bool IsFloatingBike{ get; set; }
        public bool IsCarStation{ get; set; }
        public string State{ get; set; }
    }

    public class StationsList{
        public List<ScootersDataList> Stations{ get; set; }
    }
}