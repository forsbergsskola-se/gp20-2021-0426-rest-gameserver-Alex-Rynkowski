using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Database;
using MMORPG.Items;

namespace MMORPG.Api{
    [ApiController]
    [Route("players/{id:guid}/items")]
    public class ItemController : IItemRepository{
        readonly IItemRepository itemRepository;

        public ItemController(){
            this.itemRepository = new ItemRepository();
        }


        [HttpPost("CreateItem")]
        public Task<Item> CreateItem(Guid id, string itemName, ItemTypes itemType)
            => this.itemRepository.CreateItem(id, itemName, itemType);

        [HttpDelete("DeleteItem")]
        public Task<Item> DeleteItem(Guid id, string itemName)
            => this.itemRepository.DeleteItem(id, itemName);
    }
}