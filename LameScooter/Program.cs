using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

//Assignment 3:
//https://github.com/marczaku/GP20-2021-0426-Rest-Gameserver/blob/main/assignments/assignment3.md
namespace LameScooter{
    class Program{
        static async Task Main(string[] args){
            var client =
                new MongoClient(
                    "mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false");

            var database = client.GetDatabase("LameScooters");
            var collection = database.GetCollection<BsonDocument>("inventory");

            var tmp = await collection.Find(new BsonDocument()).ToListAsync();

            Dictionary<BsonValue, Dictionary<string, BsonValue>> newDic =
                new Dictionary<BsonValue, Dictionary<string, BsonValue>>();

            foreach (var t in tmp){
                foreach (var id in t.Elements){
                    if (id.Name == "_id"){
                        newDic[id.Value] = new Dictionary<string, BsonValue>();
                        foreach (var tElement in t.Elements){
                            newDic[id.Value][tElement.Name] = tElement.Value;
                        }
                    }
                }
            }

            foreach (var (key, value) in newDic){
                if (value["bikesAvailable"] > 0)
                    Console.WriteLine($"Bikes available at- {value["name"]}: {value["bikesAvailable"]}");
            }
        }
    }
}