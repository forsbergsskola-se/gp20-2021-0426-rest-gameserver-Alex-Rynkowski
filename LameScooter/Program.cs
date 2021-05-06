using System;
using System.Text.Json;
using System.Threading.Tasks;

//Assignment 3:
//https://github.com/marczaku/GP20-2021-0426-Rest-Gameserver/blob/main/assignments/assignment3.md
namespace LameScooter{
    class Program{
        static async Task Main(string[] args){
            ILameScooterRental lameScooterRental = new LameScooterRental();

            var count = await lameScooterRental.GetScooterCountInStation(null);
            Console.WriteLine($"Number of Scooter available at this station: {count}");
            Console.WriteLine(args[0]);
        }
    }

    public class LameScooterRental : ILameScooterRental{
        public Task<int> GetScooterCountInStation(string stationName){
            throw new NotImplementedException();
        }
    }
}