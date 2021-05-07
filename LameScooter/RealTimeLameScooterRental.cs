using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LameScooter{
    public class RealTimeLameScooterRental : ILameScooterRental{
        async Task<StationsList> ScooterDataList(){
            var client = new HttpClient{
                BaseAddress = new Uri(
                    "https://raw.githubusercontent.com/marczaku/GP20-2021-0426-Rest-Gameserver/main/assignments/scooters.json")
            };
            var response = await client.GetStringAsync(client.BaseAddress);
            return JsonConvert.DeserializeObject<StationsList>(response);
        }

        public async Task<int> GetScooterCountInStation(string stationName){
            var resultList = await ScooterDataList();
            return (from s in resultList.Stations where stationName == s.Name select s.BikesAvailable).FirstOrDefault();
        }
    }
}