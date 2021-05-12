using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Api;
using MMORPG.Database;

namespace MMORPG.Controllers{
    [ApiController]
    [Route("api/players")]
    public class PlayerController{
        readonly IRepository repository;

        public PlayerController(IRepository repository){
            this.repository = repository;
        }

        [HttpGet("Get/{id:guid}")]
        public async Task<Player> Get(Guid id)
            => await this.repository.Get(id);

        [HttpGet("GetAll")]
        public Task<Player[]> GetAll()
            => this.repository.GetAll();

        [HttpPost("Create/{name}")]
        public Task<Player> Create(string name)
            => this.repository.Create(name);

        [HttpPost("Modify/{id:guid},{modifiedPlayer}")]
        public Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer)
            => this.repository.Modify(id, modifiedPlayer);

        [HttpDelete("Delete/{id:guid}")]
        public Task<Player> Delete(Guid id)
            => this.repository.Delete(id);

        [HttpPost("PurchaseLevel/{id:guid}")]
        public Task<Player> PurchaseLevel(Guid id)
            => this.repository.PurchaseLevel(id);
    }
}