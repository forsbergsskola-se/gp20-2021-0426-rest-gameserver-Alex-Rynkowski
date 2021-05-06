using System;
using System.Threading.Tasks;

namespace LameScooter{
    public class Steps{
        public static async Task Rentals(string[] args){
            try{
                switch (args[1]){
                    case "offline":
                        ILameScooterRental lameScooterRental = new OfflineLameScooterRental();
                        var offlineTimeCount = await lameScooterRental.GetScooterCountInStation(args[0]);
                        Console.WriteLine($"Number of available scooters in {args[0]}: {offlineTimeCount}");
                        break;
                    case "deprecated":
                        ILameScooterRental deprecatedScooters = new DeprecatedLameScooterRental();
                        var deprecatedCount = await deprecatedScooters.GetScooterCountInStation(args[0]);
                        Console.WriteLine($"Number of available scooters in {args[0]}: {deprecatedCount}");
                        break;
                    case "realtime":
                        ILameScooterRental realTimeScooters = new RealTimeLameScooterRental();
                        var realTimeCount = await realTimeScooters.GetScooterCountInStation(args[0]);
                        Console.WriteLine($"Number of available scooters in {args[0]}: {realTimeCount}");
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