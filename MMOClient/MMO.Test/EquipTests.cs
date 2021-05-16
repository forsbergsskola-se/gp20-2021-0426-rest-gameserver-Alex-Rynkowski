using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Model;
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

        [Test]
        public async Task EquipSwordTest(){
            await SetupTest();
            this.player1.EquippedItems = new Dictionary<string, Item>{
                [ItemTypes.Sword.ToString()] = await Equip.EquipSword(this.player1.Id, this.holySword)
            };
            Assert.AreEqual("Holy Sword", this.player1.EquippedItems[ItemTypes.Sword.ToString()].ItemName);
        }


        async Task SetupTest(){
            await ApiConnection.DeleteRequest<Player>("drop/playerCollection");
            this.player1 = await Player.Create(P1);
            this.player2 = await Player.Create(P2);
            this.player3 = await Player.Create(P3);
            
            

            this.holySword = await ItemCreator(this.player1.Id, "Holy Sword", ItemTypes.Sword, ItemRarity.Rare, 3, 2, 1000);
            await ItemCreator(this.player1.Id, "Basic Armor", ItemTypes.Armor, ItemRarity.Common, 1, 0, 20);
            await ItemCreator(this.player2.Id, "Holy Shield", ItemTypes.Shield, ItemRarity.Epic, 6, 4, 2000);
            await ItemCreator(this.player2.Id, "Dark Sword", ItemTypes.Sword, ItemRarity.Common, 1, 4, 2000);
            await ItemCreator(this.player2.Id, "Dark Armor", ItemTypes.Armor, ItemRarity.Epic, 78, 15, 20000);


            this.player1 = await Player.Get(P1);
            this.player2 = await Player.Get(P2);
            this.player3 = await Player.Get(P3);
        }

        static async Task<Item> ItemCreator(Guid playerId, string name, ItemTypes type, ItemRarity rarity,
            int levelRequirement, int levelBonus, int sellValue){
            return await Item.CreateItem(playerId, new Item{
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