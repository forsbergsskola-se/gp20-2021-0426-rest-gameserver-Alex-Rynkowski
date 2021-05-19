using System;
using System.Threading.Tasks;
using Client.Model;
using Client.RestApi;
using Client.Utilities;

namespace Client.UserStrategy{
    public class EquipStrategy{
        public async Task PlayerEquippedItems(Player player){
            Custom.WriteLine($"{player.Name}, what would you like to do?", ConsoleColor.White);
            while (true){
                Custom.WriteMultiLines(ConsoleColor.Yellow, "0: Go back", "1: Equip Sword",
                    "2: Equip Shield", "3: Equip Armor", "4: Equip Helmet");
                var userInput = Custom.ReadLine(ConsoleColor.Green);
                switch (userInput){
                    case "0":
                        return;
                    case "1":
                        await EquipSword(player.Id);
                        break;
                    case "2":
                        await EquipShield(player.Id);
                        break;
                    case "3":
                        await EquipArmor(player.Id);
                        break;
                    case "4":
                        await EquipHelmet(player.Id);
                        break;
                    default:
                        Custom.WriteLine("Unknown input", ConsoleColor.Red);
                        break;
                }
            }
        }

        static async Task<Item> Get(Guid playerId, string itemName){
            Custom.WriteLine("Enter item name to display info for that item: ", ConsoleColor.Yellow);
            return await ItemRequest.Get(playerId, itemName);
        }

        async Task EquipSword(Guid playerId){
            try{
                Custom.WriteLine("Enter sword name:", ConsoleColor.Yellow);
                var userInput = Custom.ReadLine(ConsoleColor.Green);
                var item = await Get(playerId, userInput);
                await EquipRequest.EquipSword(playerId, item);
                Custom.WriteLine($"Equipped {item.ItemName}", ConsoleColor.White);
            }
            catch (Exception){
                Custom.WriteLine("Item does not exist in your inventory", ConsoleColor.Red);
            }
        }

        async Task EquipShield(Guid playerId){
            try{
                Custom.WriteLine("Enter shield name:", ConsoleColor.Yellow);
                var userInput = Custom.ReadLine(ConsoleColor.Green);
                var item = await Get(playerId, userInput);
                await EquipRequest.EquipShield(playerId, item);
                Custom.WriteLine($"Equipped {item.ItemName}", ConsoleColor.White);
            }
            catch (Exception){
                Custom.WriteLine("Item does not exist in your inventory", ConsoleColor.Red);
            }
        }

        async Task EquipArmor(Guid playerId){
            try{
                Custom.WriteLine("Enter armor name:", ConsoleColor.Yellow);
                var userInput = Custom.ReadLine(ConsoleColor.Green);
                var item = await Get(playerId, userInput);
                await EquipRequest.EquipArmor(playerId, item);
                Custom.WriteLine($"Equipped {item.ItemName}", ConsoleColor.White);
            }
            catch (Exception){
                Custom.WriteLine("Item does not exist in your inventory", ConsoleColor.Red);
            }
        }

        async Task EquipHelmet(Guid playerId){
            try{
                Custom.WriteLine("Enter helmet name:", ConsoleColor.Yellow);
                var userInput = Custom.ReadLine(ConsoleColor.Green);
                var item = await Get(playerId, userInput);
                await EquipRequest.EquipHelmet(playerId, item);
                Custom.WriteLine($"Equipped {item.ItemName}", ConsoleColor.White);
            }
            catch (Exception){
                Custom.WriteLine("Item does not exist in your inventory", ConsoleColor.Red);
            }
        }
    }
}