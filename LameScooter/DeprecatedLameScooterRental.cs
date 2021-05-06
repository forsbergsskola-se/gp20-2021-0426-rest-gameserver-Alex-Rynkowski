using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LameScooter{
    public class DeprecatedLameScooterRental : ILameScooterRental{
        async Task<List<IScootersDataList>> ScooterDataList(){
            var dataList = new List<IScootersDataList>();
            var txtFile = await Utilities.ReadFromFile.ReadFile("scooters.txt");
            foreach (var _class in txtFile.Split("},")){
                var scooterData = new ScootersDataList();
                foreach (var variable in _class.Replace("\"", "").Split(',')){
                    var varValues = VariableValues(variable);
                    GetValuesByProperty(scooterData, varValues);
                }

                dataList.Add(scooterData);
            }

            return dataList;
        }

        static string[] VariableValues(string variable){
            var varValues = variable.Split(':')
                .Select(whiteSpace => whiteSpace.Trim())
                .Select(dots => dots.Replace('.', ',')).ToArray();
            return varValues;
        }

        static void GetValuesByProperty(IScootersDataList scooterData, string[] varValues){
            foreach (var pInfo in scooterData.GetType().GetProperties()){
                var name = char.ToLower(pInfo.Name[0]) + pInfo.Name[1..];
                if (name.Contains(varValues[0])){
                    pInfo.SetValue(scooterData, Convert.ChangeType(varValues[1], pInfo.PropertyType));
                    break;
                }
            }
        }

        public async Task<int> GetScooterCountInStation(string stationName){
            var dataList = await ScooterDataList();
            return (from data in dataList where stationName == data.Name select data.BikesAvailable).Sum();
        }
    }
}