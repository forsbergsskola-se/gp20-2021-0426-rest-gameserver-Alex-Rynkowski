using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Database;
using MMORPG.Players;

namespace MMORPG.Api{
    [ApiController]
    [Route("/api/players/{id}/items/{name}/equip")]
    public class EquippedController : IEquipped{
        [HttpPost("EquipWeapon")]
        public Task<Player> WeaponEquipped(){
            throw new System.NotImplementedException();
        }
    }
}