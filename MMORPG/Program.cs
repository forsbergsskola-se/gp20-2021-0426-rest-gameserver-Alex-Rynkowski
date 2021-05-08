using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MMORPG.Controllers;

//Color codes:
// Output White
// Yellow instructions
// Green user input
// red error or exception
namespace MMORPG{
    public class Program{
        public static async Task Main(string[] args){
            var mongo = new MongoDbRepository();
            await mongo.Create(new Player());

            //await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}