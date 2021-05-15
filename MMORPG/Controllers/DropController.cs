using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Data;
using MMORPG.Repositories;

namespace MMORPG.Controllers{
    [ApiController]
    [Route("/api/drop/")]
    public class DropController{
        readonly IRepository repository;

        public DropController(IRepository repository){
            this.repository = repository;
        }

        [HttpDelete("playerCollection")]
        public async Task DropPlayerCollection()
            => await this.repository.DropCollection.DropPlayerCollection();


        [HttpDelete("questCollection")]
        public async Task DropQuestCollection()
            => await this.repository.DropCollection.DropQuestCollection();
    }
}