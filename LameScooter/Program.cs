﻿using System;
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
                        var offlineTimeCount = await lameScooterRental.GetScooterCountInStation(args[0]);
                        Console.WriteLine($"offline____Number of available scooters in {args[0]}: {offlineTimeCount}");
                        break;
                    case "deprecated":
                        ILameScooterRental deprecatedScooters = new DeprecatedLameScooterRental();
                        var deprecatedCount = await deprecatedScooters.GetScooterCountInStation(args[0]);
                        Console.WriteLine(
                            $"deprecated____Number of available scooters in {args[0]}: {deprecatedCount}");
                        break;
                    case "realtime":
                        ILameScooterRental realTimeScooters = new RealTimeLameScooterRental();
                        var realTimeCount = await realTimeScooters.GetScooterCountInStation(args[0]);
                        Console.WriteLine($"real time____Number of available scooters in {args[0]}: {realTimeCount}");
                        break;
                    case "mongodb":
                        ILameScooterRental mongoDbScooters = new MongoDbLameScooterRental();
                        var mongoCount = await mongoDbScooters.GetScooterCountInStation(args[0]);
                        Console.WriteLine(
                            $"Mongo Db____Bikes available at {args[0]}: {mongoCount}");
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