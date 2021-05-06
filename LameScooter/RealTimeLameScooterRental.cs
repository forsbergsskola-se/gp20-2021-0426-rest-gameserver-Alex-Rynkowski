using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LameScooter{
    public class RealTimeLameScooterRental : ILameScooterRental{
        async Task<List<ScootersDataList>> ScooterDataList(){
            var client = new HttpClient{
                BaseAddress = new Uri(
                    "https://raw.githubusercontent.com/marczaku/GP20-2021-0426-Rest-Gameserver/main/assignments/scooters.json")
            };
            var response = await client.GetStringAsync(client.BaseAddress);
            return JsonSerializer.Deserialize<List<ScootersDataList>>(response);
        }

        public async Task<int> GetScooterCountInStation(string stationName){
            var resultList = await ScooterDataList();
            return (from result in resultList where stationName == result.Name select result.BikesAvailable)
                .Sum();
        }
    }
}