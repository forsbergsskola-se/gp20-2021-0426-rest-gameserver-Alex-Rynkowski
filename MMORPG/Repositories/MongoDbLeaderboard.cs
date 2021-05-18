using System.Collections.Generic;
using System.Threading.Tasks;
using MMORPG.Data;

namespace MMORPG.Repositories{
    public class MongoDbLeaderboard : ILeaderboard{
        public Task<List<Player>> TopTen(){
            var topTenList = new List<Player>();
            
            
        }
    }
}