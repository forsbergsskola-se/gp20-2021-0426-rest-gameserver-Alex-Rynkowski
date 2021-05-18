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

        public static async Task<Item> EquipShield(Guid playerId, Item item){
            return await ApiConnection.PostRequest<Item>($"/players/{playerId}/items/equip/shield",
                JsonConvert.SerializeObject(item));
        }
        
        public static async Task<Item> EquipArmor(Guid playerId, Item item){
            return await ApiConnection.PostRequest<Item>($"/players/{playerId}/items/equip/armor",
                JsonConvert.SerializeObject(item));
        }
        
        public static async Task<Item> EquipHelmet(Guid playerId, Item item){
            return await ApiConnection.PostRequest<Item>($"/players/{playerId}/items/equip/helmet",
                JsonConvert.SerializeObject(item));
        }
    }
}