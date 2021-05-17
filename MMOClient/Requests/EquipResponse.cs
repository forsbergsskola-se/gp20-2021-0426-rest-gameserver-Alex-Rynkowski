using System;
using System.Threading.Tasks;
using Client.Model;
using Client.Utilities;
using Newtonsoft.Json;

namespace Client.Requests{
    public static class EquipResponse{
        public static async Task<Item> EquipSword(Guid playerId, Item item){
            return await ApiConnection.PostRequest<Item>($"/players/{playerId}/items/equip/weapon",
                JsonConvert.SerializeObject(item));
        }
    }
}