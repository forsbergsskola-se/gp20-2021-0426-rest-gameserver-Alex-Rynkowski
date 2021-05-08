using MongoDB.Bson;
using MongoDB.Driver;

namespace MMORPG.Utilities{
    public static class DatabaseConnection{
        public static IMongoDatabase GetDatabase(){
            var client = new MongoClient("mongodb://localhost:27017");
            return client.GetDatabase("game");
        }
    }
}