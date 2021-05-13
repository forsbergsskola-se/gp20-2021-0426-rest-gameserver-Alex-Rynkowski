using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Utilities;
using MongoDB.Bson.Serialization.Attributes;

namespace Client.Api{
    public class Item{
        public Guid Id{ get; set; }
        public string ItemName{ get; set; }
        public int LevelRequirement{ get; set; }
        public int SellValue{ get; private set; }
        public int LevelBonus{ get; set; }
        public bool IsDeleted{ get; set; }
        public DateTime CreationTime{ get; set; }
        public string Rarity{ get; set; }
        public virtual string Category => this.ItemName;
        public virtual string ItemType{ get; set; }
        
        public async Task<Player> CreateItem(Guid playerId, string itemName, ItemTypes itemType ){
            //var player = await ApiConnection.GetResponse<Player>($"/Get/{id}");
            //return player;
            return default;
        }

        public async Task<List<Item>> GetAll(){
            // var players = await ApiConnection.GetResponse<List<Player>>("/GetAll");
            // return players;
            return default;
        }

        public async Task<Item> Create(string name){
            //return await ApiConnection.SendRequest<Player>($"/Create/{name}");
            return default;
        }

        public async Task<Item> Delete(Guid id){
            // var player = await ApiConnection.GetResponse<Player>($"/Delete/{id}");
            // return player;
            return default;
        }
    }
    public enum ItemTypes{
        Sword,
        Shield,
        Armor,
        Helmet,
        Potion,
    }
}