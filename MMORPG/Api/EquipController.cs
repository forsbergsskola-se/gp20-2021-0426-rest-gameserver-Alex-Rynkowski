using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Database;
using MMORPG.Items;

namespace MMORPG.Api{
    [ApiController]
    [Route("/api/players/{id}/items/equip")]
    public class EquipController : IEquip{
        readonly IEquip equipItem;

        public EquipController(){
            this.equipItem = new EquipItem();
        }


        [HttpPost("Weapon")]
        public async Task<Item> EquipSword(Guid id, string weaponName)
            => await this.equipItem.EquipSword(id, weaponName);

        [HttpPost("Shield")]
        public async Task<Item> EquipShield(Guid id, string shieldName)
            => await this.equipItem.EquipShield(id, shieldName);

        [HttpPost("Armor")]
        public async Task<Item> EquipArmor(Guid id, string armorName)
            => await this.equipItem.EquipArmor(id, armorName);

        [HttpPost("Helmet")]
        public async Task<Item> EquipHelmet(Guid id, string helmetName)
            => await this.equipItem.EquipHelmet(id, helmetName);
    }
}