using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Api;
using MMORPG.Data;
using MMORPG.Repositories;

namespace MMORPG.Controllers{
    [ApiController]
    [Route("api/players")]
    public class ItemController{
        readonly IRepository repository;

        public ItemController(IRepository repository){
            this.repository = repository;
        }

        //TODO look through the routes
        [HttpPost("{id:guid}/items/create")]
        public Task<Item> CreateItem(Guid id, ModifiedItem newItem)
            => this.repository.ItemRepository.CreateItem(id, newItem);

        [HttpDelete("{id:guid}/items/delete/{itemName}")]
        public Task DeleteItem(Guid id, string itemName)
            => this.repository.ItemRepository.DeleteItem(id, itemName);

        [HttpGet("{id:guid}/items")]
        public Task<List<Item>> GetInventory(Guid id)
            => this.repository.ItemRepository.GetInventory(id);

        [HttpGet("{id:guid}/items/get/{name}")]
        public Task<Item> GetItem(Guid id, string name)
            => this.repository.ItemRepository.GetItem(id, name);

        [HttpDelete("{id:guid}/items/{itemName}")]
        public Task<Item> SellItem(Guid id, string itemName)
            => this.repository.ItemRepository.SellItem(id, itemName);
    }
}