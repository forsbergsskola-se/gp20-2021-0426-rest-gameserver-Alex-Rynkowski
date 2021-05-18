using System;
using System.Threading.Tasks;
using Client.Model;
using Client.Requests;
using Client.RestApi;
using NUnit.Framework;

namespace MMO.Test{
    public class StatisticsTest{
        Player player1;
        Player player2;
        Player player3;

        const string P1 = "Alex R";
        const string P2 = "Marc Z";
        const string P3 = "Raphael A";

        [Test]
        public static async Task GetTotalAmountOfPlayersTest(){
            var stats = await StatisticsRequest.GetStatistics();

            Assert.AreEqual(3, stats.TotalPlayersAmount);
        }

        [Test]
        public async Task GetTotalAmountOfGoldTest(){
            await SetupTest();

            this.player1 = await PlayerResponse.Modify(this.player1.Id, new ModifiedPlayer{
                Gold = 1000,
            });
            this.player2 = await PlayerResponse.Modify(this.player2.Id, new ModifiedPlayer{
                Gold = 500,
            });
            this.player3 = await PlayerResponse.Modify(this.player3.Id, new ModifiedPlayer{
                Gold = 600,
            });

            var stats = await StatisticsRequest.GetStatistics();
            Assert.AreEqual(2100, stats.Gold);
        }

        [Test]
        public async Task GetTotalAmountOfLevelTest(){
            await SetupTest();

            this.player1 = await PlayerResponse.Modify(this.player1.Id, new ModifiedPlayer{
                Level = 50,
            });
            this.player2 = await PlayerResponse.Modify(this.player2.Id, new ModifiedPlayer{
                Level = 65,
            });
            this.player3 = await PlayerResponse.Modify(this.player3.Id, new ModifiedPlayer{
                Level = 95,
            });

            var stats = await StatisticsRequest.GetStatistics();
            Assert.AreEqual(210, stats.Level);
        }

        public async Task GetTotalAmountOfItemsTest(){
            await SetupTest();
            this.player2 = await PlayerResponse.Modify(this.player2.Id, new ModifiedPlayer{
                Level = 99,
            });
            this.player3 = await PlayerResponse.Modify(this.player3.Id, new ModifiedPlayer{
                Level = 95,
            });
            await ItemCreator(this.player1.Id, "Holy Sword", ItemTypes.Sword, ItemRarity.Rare, 3, 2, 1000);
            await ItemCreator(this.player1.Id, "Basic Armor", ItemTypes.Armor, ItemRarity.Common, 1, 0, 20);
            await ItemCreator(this.player2.Id, "Holy Shield", ItemTypes.Shield, ItemRarity.Epic, 6, 4, 2000);
            await ItemCreator(this.player2.Id, "Dark Sword", ItemTypes.Sword, ItemRarity.Common, 1, 4, 2000);
            var armor = await ItemCreator(this.player2.Id, "Dark Armor", ItemTypes.Armor, ItemRarity.Epic, 78, 15,
                20000);
            await ItemCreator(this.player3.Id, "Oh Boy", ItemTypes.Sword, ItemRarity.Rare, 8, 4, 5000);
            await ItemCreator(this.player3.Id, "Damn son", ItemTypes.Armor, ItemRarity.Common, 6, 2, 1000);
            var helmet = await ItemCreator(this.player3.Id, "Yes Box", ItemTypes.Helmet, ItemRarity.Uncommon, 78, 1,
                200);

            await EquipResponse.EquipHelmet(this.player3.Id, helmet);
            await EquipResponse.EquipArmor(this.player2.Id, armor);
            var stats = await StatisticsRequest.GetStatistics();
            Assert.AreEqual(10, stats.ItemsAmount);
        }

        async Task SetupTest(){
            await Api.DeleteRequest<Player>("drop/playerCollection");
            this.player1 = await PlayerResponse.Create(P1);
            this.player2 = await PlayerResponse.Create(P2);
            this.player3 = await PlayerResponse.Create(P3);
        }

        static async Task<Item> ItemCreator(Guid playerId, string name, ItemTypes type, ItemRarity rarity,
            int levelRequirement, int levelBonus, int sellValue){
            return await ItemResponse.CreateItem(playerId, new Item{
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