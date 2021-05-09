using System.IO;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MMORPG.Utilities{
    public static class DatabaseConnection{
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
    }
}