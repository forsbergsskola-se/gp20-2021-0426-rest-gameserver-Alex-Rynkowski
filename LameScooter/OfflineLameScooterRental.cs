using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LameScooter{
    public class OfflineLameScooterRental : ILameScooterRental{
        async Task<StationsList> ScooterDataToList(){
            return JsonConvert.DeserializeObject<StationsList>(await Utilities.ReadFromFile.ReadFile("scooter.json"));
        }

        public async Task<int> GetScooterCountInStation(string stationName){
            var scooterDataList = await ScooterDataToList();
            if (!Exists(scooterDataList.Stations, stationName)){
                throw new NotFoundException($"{stationName} does not exist");
            }

            return (from jsonData in scooterDataList.Stations
                where stationName == jsonData.Name
                select jsonData.BikesAvailable).Sum();
        }

        static bool Exists(IEnumerable<ScootersDataList> scootersDataLists, string argument){
            return scootersDataLists.Any(list => list.Name == argument);
        }
    }
}