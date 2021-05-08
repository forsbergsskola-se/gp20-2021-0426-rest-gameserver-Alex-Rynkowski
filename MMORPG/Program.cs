using System;
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
        //my ID: Guid.Parse("0801cbfe-3867-4224-9730-b7704aec44a9");
        public static async Task Main(string[] args){
            var mongo = new MongoDbRepository();
            //await mongo.Create(new Player());
            await mongo.Delete(Guid.Parse("4adf61f7-b9fe-4f19-9369-728485ca8c16"));

            //await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}