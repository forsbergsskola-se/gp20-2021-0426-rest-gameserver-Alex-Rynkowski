using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

//Assignment 3:
//https://github.com/marczaku/GP20-2021-0426-Rest-Gameserver/blob/main/assignments/assignment3.md
namespace LameScooter{
    class Program{
        static async Task Main(string[] args){
            try{
                switch (args[1]){
                    case "offline":
                        ILameScooterRental lameScooterRental = new OfflineLameScooterRental();
                        var offlineTimeCount = await lameScooterRental.GetScooterCountInStation(args[0]);
                        Console.WriteLine($"offline____Number of available scooters in {args[0]}: {offlineTimeCount}");
                        break;
                    case "deprecated":
                        ILameScooterRental deprecatedScooters = new DeprecatedLameScooterRental();
                        var deprecatedCount = await deprecatedScooters.GetScooterCountInStation(args[0]);
                        Console.WriteLine(
                            $"deprecated____Number of available scooters in {args[0]}: {deprecatedCount}");
                        break;
                    case "realtime":
                        ILameScooterRental realTimeScooters = new RealTimeLameScooterRental();
                        var realTimeCount = await realTimeScooters.GetScooterCountInStation(args[0]);
                        Console.WriteLine($"real time____Number of available scooters in {args[0]}: {realTimeCount}");
                        break;
                    case "mongodb":
                        await MongoDb(args[0]);
                        break;
                    default:
                        throw new ArgumentException("Invalid argument");
                }
            }
            catch (IndexOutOfRangeException){
                Console.WriteLine("Argument 2 has to be: \"offline\", \"deprecated\" or \"realtime\"");
                throw;
            }
        }

        static async Task MongoDb(string location){
            var client =
                new MongoClient(
                    "mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false");

            var database = client.GetDatabase("LameScooters");
            var collection = database.GetCollection<BsonDocument>("inventory");
            var inventoryList = await collection.Find(new BsonDocument()).ToListAsync();

            var idsDictionary = new Dictionary<BsonValue, Dictionary<string, BsonValue>>();

            foreach (var t in inventoryList){
                foreach (var id in t.Elements){
                    if (id.Name == "_id"){
                        idsDictionary[id.Value] = new Dictionary<string, BsonValue>();
                        foreach (var tElement in t.Elements){
                            idsDictionary[id.Value][tElement.Name] = tElement.Value;
                        }
                    }
                }
            }

            foreach (var (key, value) in idsDictionary){
                if (value["bikesAvailable"] > 0 && location == value["name"])
                    Console.WriteLine($"Mongo Db____Bikes available at {value["name"]}: {value["bikesAvailable"]}");
            }
        }
    }
}