using System;
using System.Linq;
using System.Threading.Tasks;
using Client.Model;
using Client.Requests;
using Client.Utilities;
using NUnit.Framework;

namespace MMO.Test{
    public class ItemTests{
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

        [Test]
        public async Task CommonItemTest(){
            await ApiConnection.DeleteRequest<Player>("drop/playerCollection");
            this.player1 = await Player.Create(P1);
            var commonSword = new Item{
                ItemName = "Common Sword",
                ItemType = ItemTypes.Sword,
                LevelBonus = 0,
                LevelRequirement = 3,
                Rarity = ItemRarity.Common
            };
            await ItemResponse.CreateItem(this.player1.Id, commonSword);
            this.player1 = await Player.Get(this.player1.Id);
            var playerItem = this.player1.Inventory.First(x => x.ItemName == "Common Sword");
            Assert.AreEqual(ItemTypes.Sword, playerItem.ItemType);
            Assert.AreEqual(0, playerItem.LevelBonus);
            Assert.AreEqual(3, playerItem.LevelRequirement);
            Assert.AreEqual(ItemRarity.Common, playerItem.Rarity);
        }
        [Test]
        public async Task UnCommonItemTest(){
            await ApiConnection.DeleteRequest<Player>("drop/playerCollection");
            this.player1 = await Player.Create(P1);
            var uncommonShield = new Item{
                ItemName = "UnCommon Shield",
                ItemType = ItemTypes.Shield,
                LevelBonus = 1,
                LevelRequirement = 6,
                Rarity = ItemRarity.Uncommon
            };
            await ItemResponse.CreateItem(this.player1.Id, uncommonShield);
            this.player1 = await Player.Get(this.player1.Id);
            var playerItem = this.player1.Inventory.First(x => x.ItemName == "UnCommon Shield");
            Assert.AreEqual(ItemTypes.Shield, playerItem.ItemType);
            Assert.AreEqual(1, playerItem.LevelBonus);
            Assert.AreEqual(6, playerItem.LevelRequirement);
            Assert.AreEqual(ItemRarity.Uncommon, playerItem.Rarity);
        }
        [Test]
        public async Task RareItemTest(){
            await ApiConnection.DeleteRequest<Player>("drop/playerCollection");
            this.player1 = await Player.Create(P1);
            var rareArmor = new Item{
                ItemName = "Rare Armor",
                ItemType = ItemTypes.Armor,
                LevelBonus = 2,
                LevelRequirement = 9,
                Rarity = ItemRarity.Rare
            };
            await ItemResponse.CreateItem(this.player1.Id, rareArmor);
            this.player1 = await Player.Get(this.player1.Id);
            var playerItem = this.player1.Inventory.First(x => x.ItemName == "Rare Armor");
            Assert.AreEqual(ItemTypes.Armor, playerItem.ItemType);
            Assert.AreEqual(2, playerItem.LevelBonus);
            Assert.AreEqual(9, playerItem.LevelRequirement);
            Assert.AreEqual(ItemRarity.Rare, playerItem.Rarity);
        }
        [Test]
        public async Task EpicItemTest(){
            await ApiConnection.DeleteRequest<Player>("drop/playerCollection");
            this.player1 = await Player.Create(P1);
            var epicHelmet = new Item{
                ItemName = "Epic Helmet",
                ItemType = ItemTypes.Helmet,
                LevelBonus = 5,
                LevelRequirement = 20,
                Rarity = ItemRarity.Epic
            };
            await ItemResponse.CreateItem(this.player1.Id, epicHelmet);
            this.player1 = await Player.Get(this.player1.Id);
            var playerItem = this.player1.Inventory.First(x => x.ItemName == "Epic Helmet");
            Assert.AreEqual(ItemTypes.Helmet, playerItem.ItemType);
            Assert.AreEqual(5, playerItem.LevelBonus);
            Assert.AreEqual(20, playerItem.LevelRequirement);
            Assert.AreEqual(ItemRarity.Epic, playerItem.Rarity);
        }
        
        [Test]
        public async Task PlayerOneTest(){
            await SetupTest();
            var sword = await ItemResponse.Get(this.player1.Id, this.holySword.ItemName);
            var shield = await ItemResponse.Get(this.player1.Id, this.holyShield.ItemName);
            Assert.IsNotNull(sword);
            Assert.IsNull(shield);
        }

        [Test]
        public async Task PlayerTwoTest(){
            await SetupTest();
            var sword = await ItemResponse.Get(this.player2.Id, this.holySword.ItemName);
            var shield = await ItemResponse.Get(this.player2.Id, this.holyShield.ItemName);

            Assert.IsNotNull(shield);
            Assert.IsNull(sword);
        }

        [Test]
        public async Task PlayerThreeTest(){
            await SetupTest();
            Assert.IsEmpty(this.player3.Inventory);
        }

        [Test]
        public async Task GetInventoryPlayerOne(){
            await SetupTest();
            var playerOneInventory = await ItemResponse.GetAll(this.player1.Id);

            Assert.AreEqual(2, playerOneInventory.Count);
            Assert.AreEqual(ItemTypes.Armor, playerOneInventory.First(x => x.ItemType == ItemTypes.Armor).ItemType);
            Assert.AreEqual(20, playerOneInventory.First(x => x.SellValue == 20).SellValue);
        }

        [Test]
        public async Task GetInventoryPlayerTwo(){
            await SetupTest();
            var playerOneInventory = await ItemResponse.GetAll(this.player1.Id);
            var playerTwoInventory = await ItemResponse.GetAll(this.player2.Id);

            Assert.Greater(playerTwoInventory.Count, playerOneInventory.Count);
            Assert.AreEqual(6, playerTwoInventory.First(x => x.ItemName == "Holy Shield").LevelRequirement);
            Assert.Greater(playerTwoInventory.First(x => x.ItemName == "Dark Armor").SellValue, 19000);
        }

        [Test]
        public async Task SellItemTest(){
            await SetupTest();
            Assert.AreEqual(0, this.player1.Gold);
            await ItemResponse.Sell(this.player1.Id, this.holySword.ItemName);
            this.player1 = await Player.Get(P1);
            var inventory = await ItemResponse.GetAll(this.player1.Id);
            Assert.AreEqual(1000, this.player1.Gold);
            Assert.Throws<InvalidOperationException>(() => inventory.First(x => x.ItemName == "Holy Sword"));
            Assert.ThrowsAsync<Newtonsoft.Json.JsonReaderException>(async () =>
                await ItemResponse.Sell(this.player1.Id, this.holySword.ItemName));
        }

        async Task SetupTest(){
            await ApiConnection.DeleteRequest<Player>("drop/playerCollection");
            this.player1 = await Player.Create(P1);
            this.player2 = await Player.Create(P2);
            this.player3 = await Player.Create(P3);

            this.holySword = await ItemCreator(this.player1.Id, "Holy Sword", ItemTypes.Sword, ItemRarity.Rare, 3, 2, 1000);
            this.basicArmor = await ItemCreator(this.player1.Id, "Basic Armor", ItemTypes.Armor, ItemRarity.Common, 1, 0, 20);
            this.holyShield = await ItemCreator(this.player2.Id, "Holy Shield", ItemTypes.Shield, ItemRarity.Epic, 6, 4, 2000);
            this.darkSword = await ItemCreator(this.player2.Id, "Dark Sword", ItemTypes.Sword, ItemRarity.Common, 1, 4, 2000);
            this.darkArmor = await ItemCreator(this.player2.Id, "Dark Armor", ItemTypes.Armor, ItemRarity.Epic, 78, 15, 20000);


            this.player1 = await Player.Get(P1);
            this.player2 = await Player.Get(P2);
            this.player3 = await Player.Get(P3);
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