using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MMORPG.Api;

namespace MMORPG{
    public class Program{
        //my ID: Guid.Parse("0801cbfe-3867-4224-9730-b7704aec44a9");
        public static async Task Main(string[] args){
            // var tmp = new ItemRepository();
            // await tmp.Create<Weapon>("Weapon", "Sword of a thousand truths");
            // await tmp.Create<Armor>("Armor", "Heavy Plate");
            // await tmp.Create<Helmet>("Helmet", "Sunglasses");
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}