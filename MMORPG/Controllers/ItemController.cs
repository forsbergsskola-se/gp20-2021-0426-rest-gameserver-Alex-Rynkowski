using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Api;
using MMORPG.Database;

namespace MMORPG.Controllers{
    [ApiController]
    [Route("api/players")]
    public class ItemController{
        readonly IRepository repository;

        public ItemController(IRepository repository){
            this.repository = repository;
        }

        [HttpPost("{id:guid}/items/createItem")]
        public Task<Player> CreateItem(Guid id, ModifyItem modifyItem)
            => this.repository.ItemRepository.CreateItem(id, modifyItem);

        [HttpDelete("{id:guid}/items/deleteItem/{itemName}")]
        public Task DeleteItem(Guid id, string itemName)
            => this.repository.ItemRepository.DeleteItem(id, itemName);

        [HttpGet("{id:guid}/getInventory")]
        public Task<List<Item>> GetInventory(Guid id)
            => this.repository.ItemRepository.GetInventory(id);

        [HttpGet("{id:guid}/items/getItem/{name}")]
        public Task<Item> GetItem(Guid id, string name)
            => this.repository.ItemRepository.GetItem(id, name);

        [HttpPost("{id:guid}/items/sellItem/{itemName}")]
        public Task<Item> SellItem(Guid id, string itemName)
            => this.repository.ItemRepository.SellItem(id, itemName);
    }
}