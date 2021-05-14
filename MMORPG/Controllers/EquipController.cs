using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Api;
using MMORPG.Data;
using MMORPG.Repositories;

namespace MMORPG.Controllers{
    [ApiController]
    [Route("/api/players/{id}/items/equip")]
    public class EquipController{
        readonly IRepository repository;

        public EquipController(IRepository repository){
            this.repository = repository;
        }


        [HttpPost("weapon")]
        public async Task<Item> EquipSword(Guid id, string weaponName)
            => await this.repository.EquipRepository.EquipSword(id, weaponName);

        [HttpPost("shield")]
        public async Task<Item> EquipShield(Guid id, string shieldName)
            => await this.repository.EquipRepository.EquipShield(id, shieldName);

        [HttpPost("armor")]
        public async Task<Item> EquipArmor(Guid id, string armorName)
            => await this.repository.EquipRepository.EquipArmor(id, armorName);

        [HttpPost("helmet")]
        public async Task<Item> EquipHelmet(Guid id, string helmetName)
            => await this.repository.EquipRepository.EquipHelmet(id, helmetName);
    }
}