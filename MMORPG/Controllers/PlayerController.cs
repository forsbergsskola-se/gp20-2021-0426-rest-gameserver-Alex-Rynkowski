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

        [HttpGet("get/{id:guid}")]
        public async Task<Player> Get(Guid id)
            => await this.repository.PlayerRepository.Get(id);

        [HttpGet("get/{name}")]
        public async Task<Player> Get(string name)
            => await this.repository.PlayerRepository.GetPlayerByName(name);

        [HttpGet]
        public Task<Player[]> GetAll()
            => this.repository.PlayerRepository.GetAll();

        [HttpPost("players/create")]
        public Task<Player> Create(NewPlayer name)
            => this.repository.PlayerRepository.Create(name);

        [HttpPut("{id:guid}/modify/{modifiedPlayer}")]
        public Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer)
            => this.repository.PlayerRepository.Modify(id, modifiedPlayer);

        [HttpDelete("{id:guid}/delete")]
        public Task<Player> Delete(Guid id)
            => this.repository.PlayerRepository.Delete(id);

        [HttpPost("{id:guid}/levelUp")]
        public Task<Player> LevelUp(Guid id)
            => this.repository.PlayerRepository.PurchaseLevel(id);
    }
}