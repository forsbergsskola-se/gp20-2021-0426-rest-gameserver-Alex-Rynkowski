using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Api;
using MMORPG.Database;

namespace MMORPG.Controllers{
    [ApiController]
    [Route("/api/players/{id}/items/equip")]
    public class EquipController{
        readonly IRepository repository;

        public EquipController(IRepository repository){
            this.repository = repository;
        }


        [HttpPost("Weapon")]
        public async Task<Item> EquipSword(Guid id, string weaponName)
            => await this.repository.EquipSword(id, weaponName);

        [HttpPost("Shield")]
        public async Task<Item> EquipShield(Guid id, string shieldName)
            => await this.repository.EquipShield(id, shieldName);

        [HttpPost("Armor")]
        public async Task<Item> EquipArmor(Guid id, string armorName)
            => await this.repository.EquipArmor(id, armorName);

        [HttpPost("Helmet")]
        public async Task<Item> EquipHelmet(Guid id, string helmetName)
            => await this.repository.EquipHelmet(id, helmetName);
    }
}