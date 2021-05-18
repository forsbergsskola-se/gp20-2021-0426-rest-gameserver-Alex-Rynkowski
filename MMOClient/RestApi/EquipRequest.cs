using System;
using System.Threading.Tasks;
using Client.Model;
using Newtonsoft.Json;

namespace Client.RestApi{
    public static class EquipRequest{
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