using System;
using System.Threading.Tasks;
using Client.Api;
using Client.Utilities;

//Color codes:
// Output White
// Yellow instructions
// Green user input
// red error or exception
namespace Client{
    class Program{
        static async Task Main(string[] args){
            Custom.WriteLine("\"Q[q]uit\" to stop the application from running", ConsoleColor.Yellow);
            var character = new Character();
            while (true){
                Custom.WriteLine("What would you like to do:", ConsoleColor.Yellow);
                Custom.WriteMultiLines(ConsoleColor.Yellow, "1: Create Character",
                    "2: Get Character", "3: Get all Characters");
                var userInput = Custom.ReadLine(ConsoleColor.Green);
                Custom.Exit(userInput);

                switch (userInput){
                    case "1":
                        await character.Create("SuperNoob");
                        break;
                    case "2":
                        await character.Get(Guid.Parse("5ae5e31c-ffd4-4c4e-847f-7d98f02a319c"));
                        break;
                    case "3":
                        var players = await character.GetAll();
                        foreach (var player in players){
                            Custom.WriteMultiLines(ConsoleColor.White,
                                $"Id: {player.Id}", $"Name: {player.Name}", $"Level: {player.Level}");
                        }

                        break;
                }
            }
        }
    }
}