using System;
using MMORPG.Players;
using MongoDB.Driver;

namespace MMORPG.Utilities{
    public class Db{
        public static FilterDefinition<Player> GetPlayerById(Guid id){
            return Builders<Player>.Filter.Eq(x => x.Id, id.ToString());
        }
    }
}