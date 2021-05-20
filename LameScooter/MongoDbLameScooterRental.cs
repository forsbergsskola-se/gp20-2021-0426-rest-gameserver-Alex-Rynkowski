using System.Threading.Tasks;
using MongoDB.Driver;

namespace LameScooter{
    public class MongoDbLameScooterRental : ILameScooterRental{
        static async Task<ScootersDataList> MongoDb(string valueToLookup){
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("LameScooters");
            var collection = database.GetCollection<ScootersDataList>("inventory");

            return await collection.Find(x => x.Name == valueToLookup).FirstAsync();
        }

        public async Task<int> GetScooterCountInStation(string stationName){
            var station = await MongoDb(stationName);
            return station.BikesAvailable;
        }
    }
}