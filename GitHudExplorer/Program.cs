using System;
using System.Collections.Generic;
using GitHudExplorer.Utilities;

namespace GitHudExplorer{
    class Program{
        static readonly HashSet<string> IgnoreKeyWords = new(){"components", "assets", "login", "logout", "shop"};

        static void Main(string[] args){
            Run();
            while (true){
                
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape){
            }
        }

        static async void Run(){
            var strategy = new Strategy();

            while (true){
                Custom.WriteLine("What do you want to do?", ConsoleColor.Yellow);
                Console.WriteLine("0: Check out my user (requires private access token)");
                Console.WriteLine("1: Search for another user");

                int.TryParse(Custom.ReadLine(ConsoleColor.Green), out var userChoice);

                switch (userChoice){
                    case 0:
                        await strategy.User();
                        break;
                    case 1:
                        await strategy.OtherUser();
                        break;
                    default:
                        Custom.WriteLine("Invalid input", ConsoleColor.Red);
                        break;
                }
            }
        }
    }
}