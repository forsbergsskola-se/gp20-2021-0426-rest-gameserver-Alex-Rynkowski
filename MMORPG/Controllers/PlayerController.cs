using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Api;
using MMORPG.Database;

namespace MMORPG.Controllers{
    [ApiController]
    [Route("api")]
    public class PlayerController{
        readonly IRepository repository;

        public PlayerController(IRepository repository){
            this.repository = repository;
        }

        [HttpGet("players/get/{id:guid}")]
        public async Task<Player> Get(Guid id)
            => await this.repository.PlayerRepository.Get(id);

        [HttpGet("players/getByName/{name}")]
        public async Task<Player> GetPlayerByName(string name)
            => await this.repository.PlayerRepository.GetPlayerByName(name);

        [HttpGet("players/getAll")]
        public Task<Player[]> GetAll()
            => this.repository.PlayerRepository.GetAll();

        [HttpPost("players/create/{name}")]
        public Task<Player> Create(string name)
            => this.repository.PlayerRepository.Create(name);

        [HttpPost("players/{id:guid}/modify/{modifiedPlayer}")]
        public Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer)
            => this.repository.PlayerRepository.Modify(id, modifiedPlayer);

        [HttpDelete("/players/{id:guid}/delete")]
        public Task<Player> Delete(Guid id)
            => this.repository.PlayerRepository.Delete(id);

        [HttpPost("players/{id:guid}/purchaseLevel")]
        public Task<Player> PurchaseLevel(Guid id)
            => this.repository.PlayerRepository.PurchaseLevel(id);
    }
}