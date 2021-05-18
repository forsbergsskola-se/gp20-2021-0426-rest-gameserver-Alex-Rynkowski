using System.Threading.Tasks;
using Client.Model;

namespace Client.Requests{
    public static class StatisticsRequest{
        public static async Task<PlayersStatistics> GetStatistics()
            => await RestApi.Api.GetResponse<PlayersStatistics>("statistics");
    }
}