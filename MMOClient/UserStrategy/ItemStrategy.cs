using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Model;
using Client.RestApi;
using Client.Utilities;

namespace Client.UserStrategy{
    public class ItemStrategy{
        public async Task PlayerItems(Player player){
            Custom.WriteLine($"{player.Name}, what would you like to do?", ConsoleColor.White);
            Item item;
            while (true){
                Custom.WriteMultiLines(ConsoleColor.Yellow, "0: Go back", "1: Create an item",
                    "2: Get inventory", "3: Get an item", "4: Sell an item");
                var userInput = Custom.ReadLine(ConsoleColor.Green);
                switch (userInput){
                    case "0":
                        return;
                    case "1":
                        item = await CreateItem(player);
                        if (item != null){
                            Custom.WriteLine("Item was create:", ConsoleColor.White);
                            PrintItemProperties(item);
                        }

                        break;
                    case "2":
                        var items = await GetInventory(player.Id);
                        foreach (var inventoryItem in items){
                            Custom.WriteMultiLines(ConsoleColor.White, $"Name: {inventoryItem.ItemName}",
                                $"Rarity: {inventoryItem.Rarity}", $"Type: {inventoryItem.ItemType}");
                            Console.WriteLine("-----------------------");
                        }

                        break;
                    case "3":
                        item = await Get(player.Id);
                        if (item != null){
                            Custom.WriteLine("Item info:", ConsoleColor.White);
                            PrintItemProperties(item);
                        }

                        break;
                    case "4":
                        item = await SellItem(player.Id);
                        break;
                    default:
                        Custom.WriteLine("Unknown input", ConsoleColor.Red);
                        break;
                }
            }
        }

        async Task<Item> SellItem(Guid playerId){
            Custom.WriteLine("Which item in your inventory would you like to sell? Enter item name...",
                ConsoleColor.Yellow);
            var itemName = Custom.ReadLine(ConsoleColor.Green);
            return await ItemRequest.Sell(playerId, itemName);
        }

        static void PrintItemProperties(Item item){
            Custom.WriteMultiLines(ConsoleColor.White, $"Name: \"{item.ItemName}\"",
                $"Item type: {item.ItemType}", $"Level requirement: {item.LevelRequirement}",
                $"Level bonus: {item.LevelBonus}", $"Rarity: {item.Rarity}",
                $"Sell value: {item.SellValue}");
        }

        static async Task<Item> Get(Guid playerId){
            Custom.WriteLine("Enter item name to display info for that item: ", ConsoleColor.Yellow);
            var itemName = Custom.ReadLine(ConsoleColor.Green);
            return await ItemRequest.Get(playerId, itemName);
        }

        async Task<List<Item>> GetInventory(Guid playerId){
            return await ItemRequest.GetAll(playerId);
        }

        async Task<Item> CreateItem(Player player){
            var item = new Item();

            try{
                Custom.WriteLine("Item name:", ConsoleColor.Yellow);
                item.ItemName = Custom.ReadLine(ConsoleColor.Green);

                Custom.WriteLine("Item type:", ConsoleColor.Yellow);
                var itemType = Custom.ReadLine(ConsoleColor.Green);
                if (int.TryParse(itemType, out var itemTypeResult)){
                    item.ItemType = (ItemTypes) itemTypeResult;
                }
                else{
                    item.ItemType = (ItemTypes) Enum.Parse(typeof(ItemTypes), itemType ?? string.Empty);
                }

                Custom.WriteLine("level requirement:", ConsoleColor.Yellow);
                item.LevelRequirement = Convert.ToInt32(Custom.ReadLine(ConsoleColor.Green));

                Custom.WriteLine("level bonus:", ConsoleColor.Yellow);
                item.LevelBonus = Convert.ToInt32(Custom.ReadLine(ConsoleColor.Green));

                Custom.WriteLine("item rarity:", ConsoleColor.Yellow);
                var rarity = Custom.ReadLine(ConsoleColor.Green);

                if (int.TryParse(rarity, out var result)){
                    item.Rarity = (ItemRarity) result;
                }
                else{
                    item.Rarity = (ItemRarity) Enum.Parse(typeof(ItemRarity), rarity ?? string.Empty);
                }

                Custom.WriteLine("sell value:", ConsoleColor.Yellow);
                item.SellValue = Convert.ToInt32(Custom.ReadLine(ConsoleColor.Green));
            }
            catch (Exception e){
                Custom.WriteLine($"Invalid input... {e}", ConsoleColor.Red);
                return null;
            }

            return await ItemRequest.CreateItem(player.Id, item);
        }
    }
}