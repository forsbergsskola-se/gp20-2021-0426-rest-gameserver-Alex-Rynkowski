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
            for (var i = 0; i < splitText.Length; i += 2){
                var nr = splitText[i + 1];
                var str = "";
                for (var j = 0; j < nr.Length; j++){
                    if (!char.IsDigit(nr[j])) continue;
                    str += nr[j];
                    j++;
                }
                dataList[splitText[i]] = Convert.ToInt32(str);
            }

            return dataList;
        }

        public async Task<int> GetScooterCountInStation(string stationName){
            var dataList = await ScooterDataList();
            return dataList[stationName];
        }
    }
}