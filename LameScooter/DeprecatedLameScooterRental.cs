using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LameScooter{
    public class DeprecatedLameScooterRental : ILameScooterRental{
        async Task<List<ScootersesDataList>> ScooterDataList(){
            var dataList = new List<ScootersesDataList>();
            var txtFile = await Utilities.ReadFromFile.ReadFile("scooters.txt");
            foreach (var _class in txtFile.Split("},")){
                var scooterData = new ScootersesDataList();
                foreach (var variable in _class.Replace("\"", "").Split(',')){
                    var varValues = variable.Split(':')
                        .Select(whiteSpace => whiteSpace.Trim())
                        .Select(dots => dots.Replace('.', ',')).ToArray();
                    foreach (var pInfo in scooterData.GetType().GetProperties()){
                        var name = char.ToLower(pInfo.Name[0]) + pInfo.Name[1..];
                        if (name.Contains(varValues[0])){
                            pInfo.SetValue(scooterData, Convert.ChangeType(varValues[1], pInfo.PropertyType));
                            break;
                        }
                    }
                }

                dataList.Add(scooterData);
            }

            return dataList;
        }

        public async Task<int> GetScooterCountInStation(string stationName){
            var dataList = await ScooterDataList();
            return (from data in dataList where stationName == data.Name select data.BikesAvailable).Sum();
        }
    }
}