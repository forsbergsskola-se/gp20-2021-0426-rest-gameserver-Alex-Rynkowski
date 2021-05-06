using System;
using System.Threading.Tasks;

//Assignment 3:
//https://github.com/marczaku/GP20-2021-0426-Rest-Gameserver/blob/main/assignments/assignment3.md
namespace LameScooter{
    class Program{
        static async Task Main(string[] args){
            try{
                switch (args[1]){
                    case "offline":
                        ILameScooterRental lameScooterRental = new OfflineLameScooterRental();
                        var count = await lameScooterRental.GetScooterCountInStation(args[0]);
                        Console.WriteLine($"Number of available scooters in {args[0]}: {count}");
                        break;
                    case "deprecated":
                        var scooters = new DeprecatedLameScooterRental();
                        var result = await scooters.GetScooterCountInStation(args[0]);
                        Console.WriteLine(result);
                        break;
                    case "realtime":
                        break;
                    default:
                        throw new ArgumentException("Invalid argument");
                }
            }
            catch (IndexOutOfRangeException){
                Console.WriteLine("Argument 2 has to be: \"offline\", \"deprecated\" or \"realtime\"");
                throw;
            }
        }
    }
}