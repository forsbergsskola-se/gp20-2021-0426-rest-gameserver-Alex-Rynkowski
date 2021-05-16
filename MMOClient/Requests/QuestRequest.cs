using System.Threading.Tasks;
using Client.Model;
using Client.Utilities;

namespace Client.Requests{
    public class QuestRequest{
        public static async Task<Quest> DelegateQuests()
            => await ApiConnection.GetResponse<Quest>("quests/delegateRandomQuest");
    }
}