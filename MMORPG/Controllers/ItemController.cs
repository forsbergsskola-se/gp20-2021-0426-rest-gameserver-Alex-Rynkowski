﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Api;
using MMORPG.Database;

namespace MMORPG.Controllers{
    [ApiController]
    [Route("api/players/{id:guid}/items")]
    public class ItemController{
        readonly IRepository repository;

        public ItemController(IRepository repository){
            this.repository = repository;
        }

        [HttpPost("CreateItem")]
        public Task<Player> CreateItem(Guid id, string itemName, ItemTypes itemType)
            => this.repository.CreateItem(id, itemName, itemType);

        [HttpDelete("DeleteItem")]
        public Task DeleteItem(Guid id, string itemName)
            => this.repository.DeleteItem(id, itemName);

        [HttpGet("GetInventory")]
        public Task<List<Item>> GetInventory(Guid id)
            => this.repository.GetInventory(id);

        [HttpGet("GetItem")]
        public Task<Item> GetItem(Guid id, string name)
            => this.repository.GetItem(id, name);

        [HttpPost("SellItem")]
        public Task<Item> SellItem(Guid id, string itemName)
            => this.repository.SellItem(id, itemName);
    }
}