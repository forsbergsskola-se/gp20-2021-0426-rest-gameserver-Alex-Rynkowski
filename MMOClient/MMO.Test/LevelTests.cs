using System;
using System.Threading.Tasks;
using Client.Model;
using Client.RestApi;
using Client.Utilities;
using NUnit.Framework;

namespace MMO.Test{
    public class LevelTests{
        const string P1 = "Alex R";
        const string P2 = "Marc Z";
        const string P3 = "Raphael A";

        Player player1;
        Player player2;
        Player player3;

        [Test]
        public async Task LevelUpToLevelOneTest(){
            await SetupTest();
            var quest = new Quest{
                ExpReward = 200,
                GoldReward = 1000,
                LevelRequirement = 0,
                QuestName = "Aiming for the Sky"
            };
            await QuestRequest.CompleteQuest(this.player1.Id, quest);
            await Api.PostRequest<Player>($"players/{this.player1.Id}/levelUp", "");

            this.player1 = await PlayerRequest.Get(this.player1.Id);
            Assert.AreEqual(1, this.player1.Level);
        }

        [Test]
        public async Task LevelUpToLevelTenTest(){
            await SetupTest();
            this.player1 = await PlayerRequest.Modify(this.player1.Id, new ModifiedPlayer{
                Gold = 1000,
                Level = 10,
                Score = 100
            });

            var quest = new Quest{
                ExpReward = 5000,
                GoldReward = 10000,
                LevelRequirement = 10,
                QuestName = "Aiming for the Sky"
            };

            await QuestRequest.CompleteQuest(this.player1.Id, quest);
            await Api.PostRequest<Player>($"players/{this.player1.Id}/levelUp", "");
            this.player1 = await PlayerRequest.Get(this.player1.Id);
            Assert.AreEqual(11, this.player1.Level);
        }
        
        [Test]
        public async Task LevelUpFailNotEnoughGoldTest(){
            await SetupTest();
            this.player1 = await PlayerRequest.Modify(this.player1.Id, new ModifiedPlayer{
                Gold = 0,
                Level = 10,
                Score = 100
            });

            var quest = new Quest{
                ExpReward = 5000,
                GoldReward = 100,
                LevelRequirement = 10,
                QuestName = "Aiming for the Sky"
            };

            await QuestRequest.CompleteQuest(this.player1.Id, quest);
            this.player1 = await Api.PostRequest<Player>($"players/{this.player1.Id}/levelUp", "");
            Assert.IsNull(this.player1.Name);
        }
        
        [Test]
        public async Task LevelUpFailNotEnoughExpTest(){
            await SetupTest();
            this.player1 = await PlayerRequest.Modify(this.player1.Id, new ModifiedPlayer{
                Gold = 0,
                Level = 10,
                Score = 100
            });

            var quest = new Quest{
                ExpReward = 100,
                GoldReward = 10000,
                LevelRequirement = 10,
                QuestName = "Aiming for the Sky"
            };

            await QuestRequest.CompleteQuest(this.player1.Id, quest);
            this.player1 = await Api.PostRequest<Player>($"players/{this.player1.Id}/levelUp", "");
            Assert.IsNull(this.player1.Name);
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