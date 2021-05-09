using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Utilities;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace MMORPG.Items{
    [ApiController]
    [Route("items")]
    public class ItemRepository : IItemRepository{
        readonly IMongoCollection<BsonDocument> collection;

        public ItemRepository(){
            this.collection = DatabaseConnection.GetDatabase().GetCollection<BsonDocument>("Items");
        }

        [HttpGet("Create{itemType}")]
        public async Task<IItem> Create(string itemType){
            var item = new Weapon("Ball Crusher", itemType);
            try{
                foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
                    .Where(p => typeof(IItem).IsAssignableFrom(p))){
                    if (type.Name != itemType) continue;
                    var serializedPlayer = JsonConvert.SerializeObject(item);
                    var bsonDocument = BsonDocument.Parse(serializedPlayer);
                    await this.collection.InsertOneAsync(bsonDocument);
                }
            }
            catch (Exception e){
                Console.WriteLine("Item type doesn't exist " + e);
            }

            return item;
        }
    }
}