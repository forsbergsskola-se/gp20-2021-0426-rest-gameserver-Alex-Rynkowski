using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LameScooter{
    public class MongoDbLameScooterRental : ILameScooterRental{
        static async Task<Dictionary<BsonValue, Dictionary<string, BsonValue>>> MongoDb(){
            var client =
                new MongoClient("mongodb://localhost:27017");

            var database = client.GetDatabase("LameScooters");
            var collection = database.GetCollection<BsonDocument>("inventory");

            var inventoryList = await collection.Find(new BsonDocument()).ToListAsync();
            var idsDictionary = new Dictionary<BsonValue, Dictionary<string, BsonValue>>();

            foreach (var t in inventoryList){
                foreach (var id in t.Elements){
                    if (id.Name != "_id") continue;
                    idsDictionary[id.Value] = new Dictionary<string, BsonValue>();
                    foreach (var tElement in t.Elements){
                        idsDictionary[id.Value][tElement.Name] = tElement.Value;
                    }
                }
            }


            return idsDictionary;
        }

        public async Task<int> GetScooterCountInStation(string stationName){
            var lst = await MongoDb();
            BsonValue str = 0;
            foreach (var (key, value) in lst){
                if (value["bikesAvailable"] > 0 && stationName == value["name"])
                    str = value["bikesAvailable"];
            }

            return Convert.ToInt32(str);
        }
    }
}