using System;
using MMORPG.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MMORPG.Utilities{
    public static class Db{
        public static FilterDefinition<Player> GetPlayerById(this Guid id){
            var idDef = new StringFieldDefinition<Player, Guid>(nameof(Player.Id));
            return Builders<Player>.Filter.Eq(idDef, id);
        }

        /// <summary>
        /// arg one needs to be written with a dollar sign as first character "$sample" 
        /// </summary>
        public static BsonDocument[] Pipeline(string argOne, string argTwo, BsonValue value){
            var bsonDoc = new BsonDocument{
                {
                    argOne, new BsonDocument{
                        {argTwo, value}
                    }
                }
            };
            return new[]{bsonDoc};
        }
    }
}