using System.Text.Json.Serialization;

namespace LameScooter{
    public class ScootersesDataList : IScootersDataList{
        [JsonPropertyName("id")] public string Id{ get; set; }
        [JsonPropertyName("name")] public string Name{ get; set; }
        [JsonPropertyName("x")] public float X{ get; set; }
        [JsonPropertyName("y")] public float Y{ get; set; }
        [JsonPropertyName("bikesAvailable")] public int BikesAvailable{ get; set; }
        [JsonPropertyName("spacesAvailable")] public int SpacesAvailable{ get; set; }
        [JsonPropertyName("capacity")] public int Capacity{ get; set; }
        [JsonPropertyName("allowDropoff")] public bool AllowDropoff{ get; set; }
        [JsonPropertyName("allowOverloading")] public bool AllowOverloading{ get; set; }
        [JsonPropertyName("isFloatingBike")] public bool IsFloatingBike{ get; set; }
        [JsonPropertyName("isCarStation")] public bool IsCarStation{ get; set; }
        [JsonPropertyName("state")] public string State{ get; set; }
    }
}