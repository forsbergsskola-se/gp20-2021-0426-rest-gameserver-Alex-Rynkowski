using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Model;
using Client.Utilities;
using Newtonsoft.Json;

namespace Client.Requests{
    public static class ItemResponse{
        public static async Task<Item> CreateItem(Guid playerId, Item item){
            return await ApiConnection.PostRequest<Item>($"/players/{playerId}/items/create",
                JsonConvert.SerializeObject(item));
        }

        public static async Task<List<Item>> GetAll(Guid playerId)
            => await ApiConnection.GetResponse<List<Item>>($"players/{playerId}/items");

        public static async Task<Item> Get(Guid playerId, string itemName)
            => await ApiConnection.GetResponse<Item>($"players/{playerId}/items/get/{itemName}");

        public static async Task<Item> Sell(Guid playerId, string itemName)
            => await ApiConnection.DeleteRequest<Item>($"players/{playerId}/items/{itemName}");
    }
}