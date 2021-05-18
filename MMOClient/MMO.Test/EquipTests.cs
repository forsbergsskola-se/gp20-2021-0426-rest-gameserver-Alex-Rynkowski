using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Model;
using Client.RestApi;
using Client.Utilities;
using NUnit.Framework;

namespace MMO.Test{
    public class EquipTests{
        const string P1 = "Alex R";
        const string P2 = "Marc Z";
        const string P3 = "Raphael A";

        Player player1;
        Player player2;
        Player player3;
        Item holySword;
        Item basicArmor;
        Item holyShield;
        Item darkSword;
        Item darkArmor;
        Item darkHelmet;

        [Test]
        public async Task EquipSwordTest(){
            await SetupTest();
            this.player1.EquippedItems = new Dictionary<string, Item>();
            await PlayerRequest.Modify(this.player1.Id, new ModifiedPlayer{
                Gold = 0,
                Level = 10,
                Score = 0
            });
            await EquipRequest.EquipSword(this.player1.Id, this.holySword);
            this.player1 = await PlayerRequest.Get(this.player1.Id);
            Assert.AreEqual("Holy Sword", this.player1.EquippedItems[ItemTypes.Sword.ToString()].ItemName);
        }

        [Test]
        public async Task EquipShieldTest(){
            await SetupTest();
            this.player1.EquippedItems = new Dictionary<string, Item>();
            await PlayerRequest.Modify(this.player1.Id, new ModifiedPlayer{
                Gold = 0,
                Level = 10,
                Score = 0
            });
            await EquipRequest.EquipShield(this.player1.Id, this.holySword);
            this.player1 = await PlayerRequest.Get(this.player1.Id);
            Assert.IsNull(this.player1.EquippedItems[ItemTypes.Shield.ToString()]);

            var massiveShield = await ItemCreator(this.player1.Id, "Massive Shield", ItemTypes.Shield, ItemRarity.Epic,
                3, 5,
                2000);

            await EquipRequest.EquipShield(this.player1.Id, massiveShield);
            this.player1 = await PlayerRequest.Get(this.player1.Id);
            Assert.AreEqual("Massive Shield", this.player1.EquippedItems[ItemTypes.Shield.ToString()].ItemName);
        }

        [Test]
        public async Task EquipArmorTest(){
            await SetupTest();
            this.player2.EquippedItems = new Dictionary<string, Item>();
            await PlayerRequest.Modify(this.player2.Id, new ModifiedPlayer{
                Gold = 0,
                Level = 78,
                Score = 0
            });
            await EquipRequest.EquipArmor(this.player2.Id, this.darkArmor);
            this.player2 = await PlayerRequest.Get(this.player2.Id);
            Assert.AreEqual("Dark Armor", this.player2.EquippedItems[ItemTypes.Armor.ToString()].ItemName);
        }

        [Test]
        public async Task EquipHelmetTest(){
            await SetupTest();
            this.player2.EquippedItems = new Dictionary<string, Item>();
            await PlayerRequest.Modify(this.player2.Id, new ModifiedPlayer{
                Gold = 0,
                Level = 51,
                Score = 0
            });
            await EquipRequest.EquipHelmet(this.player2.Id, this.darkHelmet);
            this.player2 = await PlayerRequest.Get(this.player2.Id);
            Assert.AreEqual("Dark Helmet", this.player2.EquippedItems[ItemTypes.Helmet.ToString()].ItemName);
        }
        
        [Test]
        public async Task SellEquippedItemTest(){
            await SetupTest();
            this.player2.EquippedItems = new Dictionary<string, Item>();
            await PlayerRequest.Modify(this.player2.Id, new ModifiedPlayer{
                Gold = 0,
                Level = 51,
                Score = 0
            });
            await EquipRequest.EquipHelmet(this.player2.Id, this.darkHelmet);
            await ItemRequest.Sell(this.player2.Id, "Dark Helmet");
            this.player2 = await PlayerRequest.Get(this.player2.Id);
            Assert.IsNull(this.player2.EquippedItems[ItemTypes.Helmet.ToString()]);
        }

        async Task SetupTest(){
            await Api.DeleteRequest<Player>("drop/playerCollection");
            this.player1 = await PlayerRequest.Create(P1);
            this.player2 = await PlayerRequest.Create(P2);
            this.player3 = await PlayerRequest.Create(P3);


            this.holySword = await ItemCreator(this.player1.Id, "Holy Sword", ItemTypes.Sword, ItemRarity.Rare, 3, 2,
                1000);
            this.basicArmor = await ItemCreator(this.player1.Id, "Basic Armor", ItemTypes.Armor, ItemRarity.Common, 1,
                0, 20);
            this.holyShield = await ItemCreator(this.player2.Id, "Holy Shield", ItemTypes.Shield, ItemRarity.Epic, 6, 4,
                2000);
            this.darkSword = await ItemCreator(this.player2.Id, "Dark Sword", ItemTypes.Sword, ItemRarity.Common, 1, 4,
                2000);
            this.darkArmor = await ItemCreator(this.player2.Id, "Dark Armor", ItemTypes.Armor, ItemRarity.Epic, 78, 15,
                20000);
            this.darkHelmet = await ItemCreator(this.player2.Id, "Dark Helmet", ItemTypes.Helmet, ItemRarity.Epic, 50, 15,
                20000);


            this.player1 = await PlayerRequest.Get(P1);
            this.player2 = await PlayerRequest.Get(P2);
            this.player3 = await PlayerRequest.Get(P3);
        }

        static async Task<Item> ItemCreator(Guid playerId, string name, ItemTypes type, ItemRarity rarity,
            int levelRequirement, int levelBonus, int sellValue){
            return await ItemRequest.CreateItem(playerId, new Item{
                ItemName = name,
                ItemType = type,
                LevelRequirement = levelRequirement,
                LevelBonus = levelBonus,
                Rarity = rarity,
                SellValue = sellValue
            });
        }
    }
}