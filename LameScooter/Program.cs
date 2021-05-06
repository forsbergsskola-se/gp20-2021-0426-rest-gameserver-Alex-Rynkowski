using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

//Assignment 3:
//https://github.com/marczaku/GP20-2021-0426-Rest-Gameserver/blob/main/assignments/assignment3.md
namespace LameScooter{
    class Program{
        static async Task Main(string[] args){
            ILameScooterRental lameScooterRental = new OfflineLameScooterRental();
            var count = await lameScooterRental.GetScooterCountInStation(args[0]);
            Console.WriteLine($"Number of available scooters in {args[0]}: {count}");
        }
    }

    public class OfflineLameScooterRental : ILameScooterRental{
        async Task<IEnumerable<ScootersesDataList>> ScooterDataToList(){
            var jsonFile = await File.ReadAllTextAsync("scooter.json");
            return JsonSerializer.Deserialize<List<ScootersesDataList>>(jsonFile);
        }

        public async Task<int> GetScooterCountInStation(string stationName){
            return (from jsonData in await ScooterDataToList()
                where stationName == jsonData.Name
                select jsonData.BikesAvailable).Sum();
        }
    }

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