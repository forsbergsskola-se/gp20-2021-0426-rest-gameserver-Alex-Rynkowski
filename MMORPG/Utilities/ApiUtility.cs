﻿using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using MMORPG.Players;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MMORPG.Utilities{
    public static class ApiUtility{
        const string JsonPath = "appsettings.json";

        public static IMongoDatabase GetDatabase(){
            var client = new MongoClient(GetDbConnection());
            return client.GetDatabase(Database());
        }

        static string GetDbConnection(){
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(JsonPath, true, true);
            return builder.Build().GetSection("MongoDatabase").GetSection("ConnectionString").Value;
        }

        static string Database(){
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(JsonPath, true, true);
            return builder.Build().GetSection("MongoDatabase").GetSection("DatabaseName").Value;
        }

        public static IMongoCollection<Player> GetPlayerCollection(){
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(JsonPath, true, true);
            var collection = builder.Build().GetSection("PlayerCollection").GetSection("CollectionString").Value;
            return GetDatabase().GetCollection<Player>(collection);
        }
    }
}