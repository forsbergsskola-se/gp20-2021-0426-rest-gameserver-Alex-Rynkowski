using System;
using System.Threading.Tasks;
using Client.Utilities;
using Newtonsoft.Json;

namespace Client.Model{
    public class Equip{
        public static async Task<Item> EquipSword(Guid playerId, Item item){
            return await ApiConnection.SendRequest<Item>($"/players/{playerId}/items/equip/weapon",
                JsonConvert.SerializeObject(item));
        }
    }
}