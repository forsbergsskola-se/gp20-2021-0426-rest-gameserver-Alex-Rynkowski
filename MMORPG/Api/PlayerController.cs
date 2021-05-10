using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Database;
using MMORPG.Players;

namespace MMORPG.Api{
    [ApiController]
    [Route("api/players")]
    public class PlayerController : IRepository{
        readonly IRepository repository;

        public PlayerController(){
            this.repository = new MongoDbRepository();
        }

        [HttpGet("Get")]
        public async Task<Player> Get(Guid id)
            => await this.repository.Get(id);

        [HttpGet("GetAll")]
        public Task<Player[]> GetAll()
            => this.repository.GetAll();

        [HttpPost("Create")]
        public Task<Player> Create(string name)
            => this.repository.Create(name);

        [HttpPost("Modify")]
        public Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer)
            => this.repository.Modify(id, modifiedPlayer);

        [HttpDelete("Delete")]
        public Task<Player> Delete(Guid id)
            => this.repository.Delete(id);
    }
}