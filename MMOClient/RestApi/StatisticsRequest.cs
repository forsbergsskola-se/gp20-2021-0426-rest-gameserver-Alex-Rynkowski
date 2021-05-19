using System.Threading.Tasks;
using Client.Model;

namespace Client.RestApi{
    public static class StatisticsRequest{
        public static async Task<PlayersStatistics> GetStatistics()
            => await Api.GetResponse<PlayersStatistics>("statistics");
    }
}