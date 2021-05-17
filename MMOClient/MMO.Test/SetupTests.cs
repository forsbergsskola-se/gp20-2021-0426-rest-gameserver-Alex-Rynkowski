using System;
using System.Threading.Tasks;
using Client.Model;
using Client.Requests;
using Client.Utilities;

namespace MMO.Test{
    public static class SetupTests{
        const string P1 = "Alex R";
        const string P2 = "Marc Z";
        const string P3 = "Raphael A";
        public static async Task Setup(Player player1, Player player2, Player player3){
            await ApiConnection.DeleteRequest<Player>("drop/playerCollection");
            player1 = await Player.Create(P1);
            player2 = await Player.Create(P2);
            player3 = await Player.Create(P3);

            await ItemCreator(player1.Id, "Holy Sword", ItemTypes.Sword, ItemRarity.Rare, 3, 2, 1000);
            await ItemCreator(player1.Id, "Basic Armor", ItemTypes.Armor, ItemRarity.Common, 1, 0, 20);
            await ItemCreator(player2.Id, "Holy Shield", ItemTypes.Shield, ItemRarity.Epic, 6, 4, 2000);
            await ItemCreator(player2.Id, "Dark Sword", ItemTypes.Sword, ItemRarity.Common, 1, 4, 2000);
            await ItemCreator(player2.Id, "Dark Armor", ItemTypes.Armor, ItemRarity.Epic, 78, 15, 20000);


            player1 = await Player.Get(player1.Id);
            player2 = await Player.Get(player2.Id);
            player3 = await Player.Get(player3.Id);
        }

        static async Task ItemCreator(Guid playerId, string name, ItemTypes type, ItemRarity rarity,
            int levelRequirement, int levelBonus, int sellValue){
            await ItemResponse.CreateItem(playerId, new Item{
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