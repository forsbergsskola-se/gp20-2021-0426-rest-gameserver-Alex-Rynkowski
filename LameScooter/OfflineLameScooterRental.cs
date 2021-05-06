using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LameScooter{
    public class OfflineLameScooterRental : ILameScooterRental{
        async Task<List<ScootersesDataList>> ScooterDataToList(){
            return JsonSerializer.Deserialize<List<ScootersesDataList>>(
                await Utilities.ReadFromFile.ReadFile("scooter.json"));
        }

        public async Task<int> GetScooterCountInStation(string stationName){
            var scooterDataList = await ScooterDataToList();
            if (!Exists(scooterDataList, stationName)){
                throw new NotFoundException($"{stationName} does not exist");
            }

            return (from jsonData in scooterDataList
                where stationName == jsonData.Name
                select jsonData.BikesAvailable).Sum();
        }

        static bool Exists(IEnumerable<ScootersesDataList> scootersDataLists, string argument){
            return scootersDataLists.Any(list => list.Name == argument);
        }
    }
}