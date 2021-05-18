using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Model;
using Newtonsoft.Json;

namespace Client.RestApi{
    public static class ItemRequest{
        public static async Task<Item> CreateItem(Guid playerId, Item item){
            return await RestApi.Api.PostRequest<Item>($"/players/{playerId}/items/create",
                JsonConvert.SerializeObject(item));
        }

        public static async Task<List<Item>> GetAll(Guid playerId)
            => await RestApi.Api.GetResponse<List<Item>>($"players/{playerId}/items");

        public static async Task<Item> Get(Guid playerId, string itemName)
            => await RestApi.Api.GetResponse<Item>($"players/{playerId}/items/get/{itemName}");

        public static async Task<Item> Sell(Guid playerId, string item)
            => await RestApi.Api.DeleteRequest<Item>($"players/{playerId}/items/{item}");
    }
}