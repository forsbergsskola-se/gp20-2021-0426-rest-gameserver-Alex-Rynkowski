using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LameScooter{
    public class DeprecatedLameScooterRental : ILameScooterRental{
        async Task<Dictionary<string, int>> ScooterDataList(){
            var dataList = new Dictionary<string, int>();
            var txtFile = await Utilities.ReadFromFile.ReadFile("scooters.txt");
            var splitText = txtFile.Split(" : ");
            for (var i = 0; i < splitText.Length; i++){
                if (i + 1 == splitText.Length) break;
                if (i < 0) continue;
                var nr = splitText[i + 1];
                var txt = splitText[i];
                var str = txt.Where(t => !char.IsDigit(t)).Aggregate("", (current, t) => current + t);

                var strNr = nr.Where(char.IsDigit).Aggregate("", (current, t) => current + t);

                dataList[str.Trim()] = Convert.ToInt32(strNr);
            }

            return dataList;
        }

        public async Task<int> GetScooterCountInStation(string stationName){
            var dataList = await ScooterDataList();
            return dataList[stationName];
        }
    }
}