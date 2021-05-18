using System.Threading.Tasks;
using Client.Api;
using Client.Model;
using Client.RestApi;
using Client.Utilities;

namespace Client.Requests{
    public static class StatisticsRequest{
        public static async Task<PlayersStatistics> GetStatistics()
            => await RestApi.Api.GetResponse<PlayersStatistics>("statistics");
    }
}