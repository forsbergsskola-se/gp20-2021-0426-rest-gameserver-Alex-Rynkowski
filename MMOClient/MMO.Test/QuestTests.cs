using System;
using System.Threading.Tasks;
using Client.Api;
using Client.Model;
using Client.Requests;
using Client.RestApi;
using Client.Utilities;
using NUnit.Framework;

namespace MMO.Test{
    public class QuestTests{
        const int QuestDelayMilliSeconds = 3000; 
        const string P1 = "Alex R";
        const string P2 = "Marc Z";
        const string P3 = "Raphael A";

        Player player1;
        Player player2;
        Player player3;

        [Test]
        public async Task GetOneQuestsTest(){
            await SetupTest();
            await Task.Delay(QuestDelayMilliSeconds);
            this.player1 = await PlayerRequest.Get(this.player1.Id);
            Assert.IsNotNull(this.player1.Quests[0]);
            Assert.IsNull(this.player1.Quests[1]);
            Assert.IsNull(this.player1.Quests[2]);
            Assert.IsNull(this.player1.Quests[3]);
            Assert.IsNull(this.player1.Quests[4]);
        }
        [Test]
        public async Task GetTwoQuestsTest(){
            await SetupTest();
            await Task.Delay(QuestDelayMilliSeconds * 2);
            this.player1 = await PlayerRequest.Get(this.player1.Id);
            Assert.IsNotNull(this.player1.Quests[0]);
            Assert.IsNotNull(this.player1.Quests[1]);
            Assert.IsNull(this.player1.Quests[2]);
            Assert.IsNull(this.player1.Quests[3]);
            Assert.IsNull(this.player1.Quests[4]);
        }
        [Test]
        public async Task GetThreeQuestsTest(){
            await SetupTest();
            await Task.Delay(QuestDelayMilliSeconds * 3);
            this.player1 = await PlayerRequest.Get(this.player1.Id);
            Assert.IsNotNull(this.player1.Quests[0]);
            Assert.IsNotNull(this.player1.Quests[1]);
            Assert.IsNotNull(this.player1.Quests[2]);
            Assert.IsNull(this.player1.Quests[3]);
            Assert.IsNull(this.player1.Quests[4]);
        }
        [Test]
        public async Task GetFourQuestsTest(){
            await SetupTest();
            await Task.Delay(QuestDelayMilliSeconds * 4);
            this.player1 = await PlayerRequest.Get(this.player1.Id);
            Assert.IsNotNull(this.player1.Quests[0]);
            Assert.IsNotNull(this.player1.Quests[1]);
            Assert.IsNotNull(this.player1.Quests[2]);
            Assert.IsNotNull(this.player1.Quests[3]);
            Assert.IsNull(this.player1.Quests[4]);
        }
        [Test]
        public async Task GetFiveQuestsTest(){
            await SetupTest();
            await Task.Delay(QuestDelayMilliSeconds * 5);
            this.player1 = await PlayerRequest.Get(this.player1.Id);
            Assert.IsNotNull(this.player1.Quests[0]);
            Assert.IsNotNull(this.player1.Quests[1]);
            Assert.IsNotNull(this.player1.Quests[2]);
            Assert.IsNotNull(this.player1.Quests[3]);
            Assert.IsNotNull(this.player1.Quests[4]);
        }
        [Test]
        public async Task CompleteQuestTest(){
            await SetupTest();
            this.player1 = await PlayerRequest.Modify(this.player1.Id, new ModifiedPlayer{
                Gold = 1000,
                Level = 5,
                Score = 100
            });
            var quest = new Quest{
                ExpReward = 2000,
                GoldReward = 1000,
                LevelRequirement = 5,
                QuestName = "Sky"
            };
            
            await QuestRequest.CompleteQuest(this.player1.Id, quest);
            this.player1 = await PlayerRequest.Get(this.player1.Id);
            
            Assert.AreEqual(2000, this.player1.Gold);
        }

        async Task SetupTest(){
            await Api.DeleteRequest<Player>("drop/playerCollection");
            this.player1 = await PlayerRequest.Create(P1);
            this.player2 = await PlayerRequest.Create(P2);
            this.player3 = await PlayerRequest.Create(P3);

            await ItemCreator(this.player1.Id, "Holy Sword", ItemTypes.Sword, ItemRarity.Rare, 3, 2, 1000);
            await ItemCreator(this.player1.Id, "Basic Armor", ItemTypes.Armor, ItemRarity.Common, 1, 0, 20);
            await ItemCreator(this.player2.Id, "Holy Shield", ItemTypes.Shield, ItemRarity.Epic, 6, 4, 2000);
            await ItemCreator(this.player2.Id, "Dark Sword", ItemTypes.Sword, ItemRarity.Common, 1, 4, 2000);
            await ItemCreator(this.player2.Id, "Dark Armor", ItemTypes.Armor, ItemRarity.Epic, 78, 15, 20000);
        }

        static async Task ItemCreator(Guid playerId, string name, ItemTypes type, ItemRarity rarity,
            int levelRequirement, int levelBonus, int sellValue){
            await ItemRequest.CreateItem(playerId, new Item{
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