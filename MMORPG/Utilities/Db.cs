using System;
using MMORPG.Database;
using MongoDB.Driver;

namespace MMORPG.Utilities{
    public static class Db{
        public static FilterDefinition<Player> GetPlayerById(Guid id){
            var idDef = new StringFieldDefinition<Player, Guid>(nameof(Player.Id));
            return Builders<Player>.Filter.Eq(idDef, id);
        }
    }
}