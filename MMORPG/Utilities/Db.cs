using System;
using MMORPG.Data;
using MongoDB.Driver;

namespace MMORPG.Utilities{
    public static class Db{
        public static FilterDefinition<Player> GetPlayerById(this Guid id){
            var idDef = new StringFieldDefinition<Player, Guid>(nameof(Player.Id));
            return Builders<Player>.Filter.Eq(idDef, id);
        }
    }
}