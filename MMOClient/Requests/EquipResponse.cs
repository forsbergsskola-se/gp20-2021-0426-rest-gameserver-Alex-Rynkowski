using System;
using System.Threading.Tasks;
using Client.Api;
using Client.Model;
using Client.RestApi;
using Client.Utilities;
using Newtonsoft.Json;

namespace Client.Requests{
    public static class EquipResponse{
        public static async Task<Item> EquipSword(Guid playerId, Item item){
            return await RestApi.Api.PostRequest<Item>($"/players/{playerId}/items/equip/weapon",
                JsonConvert.SerializeObject(item));
        }

        public static async Task<Item> EquipShield(Guid playerId, Item item){
            return await RestApi.Api.PostRequest<Item>($"/players/{playerId}/items/equip/shield",
                JsonConvert.SerializeObject(item));
        }
        
        public static async Task<Item> EquipArmor(Guid playerId, Item item){
            return await RestApi.Api.PostRequest<Item>($"/players/{playerId}/items/equip/armor",
                JsonConvert.SerializeObject(item));
        }
        
        public static async Task<Item> EquipHelmet(Guid playerId, Item item){
            return await RestApi.Api.PostRequest<Item>($"/players/{playerId}/items/equip/helmet",
                JsonConvert.SerializeObject(item));
        }
    }
}